using System.Text;
using Dapper;
using Microsoft.Data.Sqlite;
using RMS.DATA.BaseServices;
using RMS.DATA.Entities;
using RMS.DATA.Repositories;
using RMS.DATA.Services;

namespace RMS.DATA
{
    public class DbContext
    {
        private readonly DbConfig dbConfig;
        public DbContext(DbConfig dbConfig)
        {
            this.dbConfig = dbConfig;
            Groups = new GroupService(dbConfig, new GroupRepository());
            DateOps = new DateOpSercive(dbConfig, new DateOpRepository());
        }

        public IServiceStandart<Group> Groups { get; private set; }
        public IServiceStandart<DateOp> DateOps { get; private set; }

        public async Task Setup()
        {
            try
            {
                StringBuilder sql = new();
                sql.Append(" CREATE TABLE IF NOT EXISTS [Group] (                                                   ");
                sql.Append("    GroupId     INTEGER       NOT NULL,                                                 ");
                sql.Append("    Name        VARCHAR(100)  NOT NULL,                                                 ");
                sql.Append("    Comment     TEXT,                                                                   ");
                sql.Append("    PRIMARY KEY(GroupId AUTOINCREMENT)                                                  ");
                sql.Append(" );                                                                                     ");
                
                sql.Append(" INSERT OR REPLACE INTO [Group](GroupId, Name, Comment)                                 ");
                sql.Append("    VALUES(1,                                                                           ");
                sql.Append("            'ПУСТО',                                                                    ");
                sql.Append("            (SELECT Comment FROM [Group] WHERE GroupId = 1)                             ");
                sql.Append(" );                                                                                     ");

                sql.Append(" CREATE TABLE IF NOT EXISTS [Office] (                                                  ");
                sql.Append("    OfficeId    INTEGER       NOT NULL,                                                 ");
                sql.Append("    Name        VARCHAR(20)   NOT NULL,                                                 ");
                sql.Append("    PRIMARY KEY(OfficeId AUTOINCREMENT)                                                 ");
                sql.Append(" );                                                                                     ");

                sql.Append(" INSERT OR REPLACE INTO [Office](OfficeId, Name)                                        ");
                sql.Append("    VALUES(1,'ПУСТО');                                                                  ");

                sql.Append(" CREATE TABLE IF NOT EXISTS [Manager] (                                                 ");
                sql.Append("    ManagerId   INTEGER       NOT NULL,                                                 ");
                sql.Append("    Name        VARCHAR(100)  NOT NULL,                                                 ");
                sql.Append("    PRIMARY KEY(ManagerId AUTOINCREMENT)                                                ");
                sql.Append(" );                                                                                     ");

                sql.Append(" INSERT OR REPLACE INTO [Manager](ManagerId, Name)                                      ");
                sql.Append("    VALUES(1,'ПУСТО');                                                                  ");

                sql.Append(" CREATE TABLE IF NOT EXISTS [Company](                                                  ");
                sql.Append("    CompanyId    INTEGER        PRIMARY KEY                                             ");
                sql.Append("                                NOT NULL,                                               ");
                sql.Append("    ManagerId    INTEGER        NOT NULL                                                ");
                sql.Append("                                REFERENCES [Manager](ManagerId) ON DELETE SET DEFAULT   ");
                sql.Append("                                                                ON UPDATE CASCADE       ");
                sql.Append("                                DEFAULT(1),                                             ");
                sql.Append("    GroupId      INTEGER        NOT NULL                                                ");
                sql.Append("                                REFERENCES[Group](GroupId)   ON DELETE SET DEFAULT      ");
                sql.Append("                                                             ON UPDATE CASCADE          ");
                sql.Append("                                DEFAULT(1),                                             ");
                sql.Append("    Name         VARCHAR(255)   NOT NULL,                                               ");
                sql.Append("    IsActive     BOOLEAN        NOT NULL                                                ");
                sql.Append("                                DEFAULT(1),                                             ");
                sql.Append("    IsAttraction BOOLEAN        NOT NULL                                                ");
                sql.Append("                                DEFAULT(0),                                             ");
                sql.Append("    Inn          CHARACTER(12)  NOT NULL                                                ");
                sql.Append("                                UNIQUE,                                                 ");
                sql.Append("    Comment      VARCHAR(255)                                                           ");
                sql.Append(" );                                                                                     ");

                sql.Append(" CREATE TABLE IF NOT EXISTS [Account] (                                                 ");
                sql.Append("    AccountId        INTEGER    NOT NULL,                                               ");
                sql.Append("    CompanyId        INTEGER    NOT NULL                                                ");
                sql.Append("                                REFERENCES [Company](CompanyId) ON DELETE CASCADE       ");
                sql.Append("                                                                ON UPDATE CASCADE,      ");
                sql.Append("    OfficeId         INTEGER    REFERENCES [Office](OfficeId)   ON DELETE SET DEFAULT   ");
                sql.Append("                                                                ON UPDATE CASCADE       ");
                sql.Append("                                NOT NULL                                                ");
                sql.Append("                                DEFAULT(1),                                             ");
                sql.Append("    DateOpen         DATE       NOT NULL,                                               ");
                sql.Append("    DateClose        DATE,                                                              ");
                sql.Append("    DateTimeLastOperation       DATETIME,                                               ");
                sql.Append("    AccountNumber               CHARACTER(20) NOT NULL,                                 ");
                sql.Append("    PRIMARY KEY(AccountId AUTOINCREMENT)                                                ");
                sql.Append(" );                                                                                     ");
                
                sql.Append(" CREATE TABLE IF NOT EXISTS [Acquiring] (                                               ");
                sql.Append("    AccountId        INTEGER        PRIMARY KEY                                         ");
                sql.Append("                                    NOT NULL                                            ");
                sql.Append("                                    REFERENCES [Account](AccountId) ON DELETE CASCADE   ");
                sql.Append("                                                                    ON UPDATE CASCADE,  ");
                sql.Append("    Comission        NUMERIC(6, 4)  NOT NULL,                                           ");
                sql.Append("    Tarif            NUMERIC(6, 4)  NOT NULL,                                           ");
                sql.Append("    WriteOffOther    NUMERIC(5, 2)  NOT NULL,                                           ");
                sql.Append("    IsActive         BOOLEAN        NOT NULL                                            ");
                sql.Append("                                    DEFAULT(1)                                          ");
                sql.Append(" );                                                                                     ");

                sql.Append(" CREATE TABLE IF NOT EXISTS [Currency] (                                                ");
                sql.Append("    Iso              CHARACTER(3)   NOT NULL                                            ");
                sql.Append("                                    PRIMARY KEY,                                        ");
                sql.Append("    Code             VARCHAR(4)     UNIQUE                                              ");
                sql.Append("                                    NOT NULL,                                           ");
                sql.Append("    Description      TEXT           NOT NULL                                            ");
                sql.Append(" );                                                                                     ");

                sql.Append(" CREATE TABLE IF NOT EXISTS [DateOp] (                                                  ");
                sql.Append("    DateId           INTEGER        NOT NULL,                                           ");
                sql.Append("    DateOperation    DATE           NOT NULL,                                           ");
                sql.Append("    PRIMARY KEY(DateId AUTOINCREMENT)                                                   ");
                sql.Append(" );                                                                                     ");

                sql.Append(" CREATE TABLE IF NOT EXISTS [ExchangeRate] (                                            ");
                sql.Append("    Iso              CHARACTER(3)   NOT NULL                                            ");
                sql.Append("                                    REFERENCES [Currency](Iso)  ON DELETE CASCADE       ");
                sql.Append("                                                                ON UPDATE CASCADE,      ");
                sql.Append("    DateId           INTEGER        NOT NULL                                            ");
                sql.Append("                                    REFERENCES [DateOp](DateId) ON DELETE CASCADE       ");
                sql.Append("                                                                ON UPDATE CASCADE,      ");
                sql.Append("    Rate             NUMERIC(7, 4)  NOT NULL,                                           ");
                sql.Append("    PRIMARY KEY(Iso, DateId)                                                            ");
                sql.Append(" );                                                                                     ");

                sql.Append(" CREATE TABLE IF NOT EXISTS [Operation] (                                               ");
                sql.Append("    DateOfUnloading  DATE           NOT NULL,                                           ");
                sql.Append("    Amount           NUMERIC(15,2)  NOT NULL,                                           ");
                sql.Append("    AmountEquivalent NUMIRIC(15,2)  NOT NULL,                                           ");
                sql.Append("    Purpose          TEXT           NOT NULL,                                           ");
                sql.Append("    Payer            CHARACTER(20)  NOT NULL,                                           ");
                sql.Append("    Recipient        CHARACTER(20)  NOT NULL,                                           ");
                sql.Append("    DateOperation    DATE           NOT NULL                                            ");
                sql.Append(" );                                                                                     ");
                
                sql.Append(" CREATE TABLE IF NOT EXISTS [Conversion] (                                              ");
                sql.Append("    DateOperation               DATE            NOT NULL,                               ");
                sql.Append("    TypeOfTransaction           VARCHAR(30)     NOT NULL,                               ");
                sql.Append("    ReceivesAmount              NUMERIC(15,2)   NOT NULL,                               ");
                sql.Append("    ReceivedCurrency            CHARACTER(4)    NOT NULL,                               ");
                sql.Append("    GivesAmount                 NUMERIC(15,2)   NOT NULL,                               ");
                sql.Append("    GivesCurrency               CHARACTER(4)    NOT NULL,                               ");
                sql.Append("    RateCurrencyOfCrediting     NUMERIC(15,2)   NOT NULL,                               ");
                sql.Append("    RateCurrencyOfDebiting      NUMERIC(15,2)   NOT NULL,                               ");
                sql.Append("    ReceivesToAccount           CHARACTER(20)   NOT NULL,                               ");
                sql.Append("    GivesFromAccount            CHARACTER(20)   NOT NULL                                ");
                sql.Append(" );                                                                                     ");
                
                sql.Append(" CREATE TABLE IF NOT EXISTS [Remains] (                                                 ");
                sql.Append("    DateOfUnloading  DATE           NOT NULL,                                           ");
                sql.Append("    Account          CHARACTER(20)  NOT NULL,                                           ");
                sql.Append("    Debit            NUMIRIC(15,2)  NOT NULL,                                           ");
                sql.Append("    Credit           NUMIRIC(15,2)  NOT NULL,                                           ");
                sql.Append("    AverageBalance   NUMERIC(15,2)  NOT NULL                                            ");
                sql.Append(" );                                                                                     ");

                sql.Append(" CREATE TABLE IF NOT EXISTS [RemainsDeposit] (                                          ");
                sql.Append("    DateOfUnloading  DATE           NOT NULL,                                           ");
                sql.Append("    Account          CHARACTER(20)  NOT NULL,                                           ");
                sql.Append("    Debit            NUMIRIC(15,2)  NOT NULL,                                           ");
                sql.Append("    Credit           NUMIRIC(15,2)  NOT NULL,                                           ");
                sql.Append("    AverageBalance   NUMERIC(15,2)  NOT NULL                                            ");
                sql.Append(" );                                                                                     ");

                sql.Append(" CREATE TABLE IF NOT EXISTS [MaskType] (                                                ");
                sql.Append("    MaskTypeId  INTEGER       NOT NULL,                                                 ");
                sql.Append("    Name        VARCHAR(100)  NOT NULL,                                                 ");
                sql.Append("    PRIMARY KEY(MaskId AUTOINCREMENT)                                                   ");
                sql.Append(" );                                                                                     ");

                sql.Append(" CREATE TABLE IF NOT EXISTS [Mask] (                                                    ");
                sql.Append("    MaskId           INTEGER        NOT NULL,                                           ");
                sql.Append("    MaskTypeId       INTEGER        NOT NULL                                            ");
                sql.Append("                                    REFERENCES [MaskType](MaskTypeId) ON DELETE CASCADE ");
                sql.Append("                                                                      ON UPDATE CASCADE,");
                sql.Append("    Content          VARCHAR(100)   NOT NULL,                                           ");
                sql.Append("    PRIMARY KEY(MaskId AUTOINCREMENT)                                                   ");
                sql.Append(" );                                                                                     ");

                using var connection = new SqliteConnection(dbConfig.Name);
                await connection.ExecuteAsync(sql.ToString()).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
