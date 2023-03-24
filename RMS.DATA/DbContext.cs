using System.Text;
using Dapper;
using Microsoft.Data.Sqlite;
using RMS.DATA.BaseServices;
using RMS.DATA.Entities;
using RMS.DATA.Repositories;
using RMS.DATA.Services;
using RMS.DATA.Views;

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
            Companies = new CompanyService(dbConfig, new CompanyRepository());
            ViewCompanies = new CompanyViewService(dbConfig, new CompanyViewRepository());
            Managers = new ManagerService(dbConfig, new ManagerRepository());
            Accounts = new AccountService(dbConfig, new AccountRepository());
            Offices = new OfficeService(dbConfig,new OfficeRepository());
            Acquirings = new AcquiringService(dbConfig, new AcquiringRepository());
            Remains = new RemainsService(dbConfig, new RemainsRepository());
            RemainsDeposit = new RemainsService(dbConfig, new RemainsDepositRepository());
            Operations = new OperationService(dbConfig, new OperationRepository());
            Conversions = new ConversionService(dbConfig, new ConversionRepository());
            MaskTypes = new MaskTypeService(dbConfig, new MaskTypeRepository());
            Masks = new MaskService(dbConfig, new MaskRepository());
        }

        public IServiceStandart<Group> Groups { get; private set; }
        public IServiceStandart<DateOp> DateOps { get; private set; }
        public IServiceStandart<Manager> Managers { get; private set; }
        public IServiceStandart<Office> Offices { get; private set; }
        public IServiceExtended<Company, int, string> Companies { get; private set;}
        public IServiceFind<CompanyView, CompanyView> ViewCompanies { get; private set; }
        public IServiceExtended<Account, int, string> Accounts { get; private set; }
        public IServiceMin<Acquiring, int> Acquirings { get; private set; }
        public IServiceUnload<Remains, DateTime, DateTime> Remains { get; private set; }
        public IServiceUnload<Remains, DateTime, DateTime> RemainsDeposit { get; private set; }
        public IServiceUnload<Operation, DateTime, DateTime> Operations { get; private set; }
        public IServiceUnload<Conversion, DateTime, DateTime> Conversions { get; private set; }
        public IServiceStandart<MaskType> MaskTypes { get; private set; }
        public IServiceExtended<Mask, int, string> Masks { get; private set; }

        public void Setup()
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
                sql.Append("    Inn          CHARACTER(12)  NOT NULL,                                               ");
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
                sql.Append("    DateOpen         DATE,                                                              ");
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

                sql.Append(" INSERT OR REPLACE INTO [Currency] (Iso, Code, Description)                             ");
                sql.Append("    VALUES('810', 'RUB', 'Российский рубль'),                                           ");
                sql.Append("                    ('978', 'EUR', 'Евро'),                                             ");
                sql.Append("                    ('840', 'USD', 'Доллар США'),                                       ");
                sql.Append("                    ('784', 'AED', 'ДИРХАМ'),                                           ");
                sql.Append("                    ('008', 'ALL', 'Албанский лек'),                                    ");
                sql.Append("                    ('051', 'AMD', 'АРМЯНСКИЙ ДРАМ'),                                   ");
                sql.Append("                    ('532', 'ANG', 'Нидерландский антильский гульден'),                 ");
                sql.Append("                    ('032', 'ARS', 'АРГЕНТИНСКОЕ ПЕСО'),                                ");
                sql.Append("                    ('036', 'AUD', 'Австралийский доллар'),                             ");
                sql.Append("                    ('944', 'AZN', 'Азербайджанский манат'),                            ");
                sql.Append("                    ('100', 'BGL', 'Лев'),                                              ");
                sql.Append("                    ('975', 'BGN', 'БОЛГАРСКИЙ ЛЕВ'),                                   ");
                sql.Append("                    ('048', 'BHD', 'БАХРЕЙНСКИЙ ДИНАР'),                                ");
                sql.Append("                    ('986', 'BRL', 'БРАЗИЛЬСКИЙ РЕАЛ'),                                 ");
                sql.Append("                    ('933', 'BYN', 'БЕЛОРУССКИЙ РУБЛЬ'),                                ");
                sql.Append("                    ('974', 'BYR', 'Белорусский рубль'),                                ");
                sql.Append("                    ('124', 'CAD', 'Канадский доллар'),                                 ");
                sql.Append("                    ('976', 'CDF', 'КОНГОЛЕЗСКИЙ ФРАНК'),                               ");
                sql.Append("                    ('756', 'CHF', 'Швейцарский франк'),                                ");
                sql.Append("                    ('152', 'CLP', 'ЧИЛИЙСКОЕ ПЕСО'),                                   ");
                sql.Append("                    ('156', 'CNY', 'Китайский юань'),                                   ");
                sql.Append("                    ('188', 'CRC', 'Костариканский колон'),                             ");
                sql.Append("                    ('203', 'CZK', 'Чешская крона'),                                    ");
                sql.Append("                    ('208', 'DKK', 'Датская крона'),                                    ");
                sql.Append("                    ('214', 'DOP', 'ДОМИНИКАНСКОЕ ПЕСО'),                               ");
                sql.Append("                    ('012', 'DZD', 'Алжирский динар'),                                  ");
                sql.Append("                    ('818', 'EGP', 'ЕГИПЕТСКИЙ ФУНТ'),                                  ");
                sql.Append("                    ('826', 'GBP', 'Фунт стерлингов'),                                  ");
                sql.Append("                    ('268', 'GEK', 'Грузинский купон'),                                 ");
                sql.Append("                    ('981', 'GEL', 'Грузинские лари'),                                  ");
                sql.Append("                    ('936', 'GHS', 'ГАНСКИЙ СЕДИ'),                                     ");
                sql.Append("                    ('300', 'GRD', 'Драхма'),                                           ");
                sql.Append("                    ('328', 'GYD', 'Гайанский доллар'),                                 ");
                sql.Append("                    ('344', 'HKD', 'Гонконгский доллар'),                               ");
                sql.Append("                    ('191', 'HRK', 'Куна'),                                             ");
                sql.Append("                    ('348', 'HUF', 'Форинт'),                                           ");
                sql.Append("                    ('360', 'IDR', 'Рупия'),                                            ");
                sql.Append("                    ('376', 'ILS', 'НОВЫЙ ИЗРАИЛЬСКИЙ ШЕКЕЛЬ'),                         ");
                sql.Append("                    ('356', 'INR', 'Индийская рупия'),                                  ");
                sql.Append("                    ('364', 'IRR', 'Иранский риал'),                                    ");
                sql.Append("                    ('352', 'ISK', 'ИСЛАНДСКАЯ КРОНА'),                                 ");
                sql.Append("                    ('392', 'JPY', 'Иена'),                                             ");
                sql.Append("                    ('417', 'KGS', 'Сом (киргизский)'),                                 ");
                sql.Append("                    ('408', 'KPW', 'Северо - корейская'),                               ");
                sql.Append("                    ('410', 'KRW', 'Вона'),                                             ");
                sql.Append("                    ('414', 'KWD', 'Кувейтский динар'),                                 ");
                sql.Append("                    ('398', 'KZT', 'ТЕНГЕ'),                                            ");
                sql.Append("                    ('144', 'LKR', 'ШРИ-ЛАНКИЙСКАЯ РУПИЯ'),                             ");
                sql.Append("                    ('426', 'LSL', 'Лоти'),                                             ");
                sql.Append("                    ('440', 'LTL', 'Литовский лит'),                                    ");
                sql.Append("                    ('428', 'LVL', 'Латвийский лат'),                                   ");
                sql.Append("                    ('504', 'MAD', 'Марокканский дирхам'),                              ");
                sql.Append("                    ('498', 'MDL', 'Молдавский лей'),                                   ");
                sql.Append("                    ('104', 'MMK', 'КЬЯТ'),                                             ");
                sql.Append("                    ('446', 'MOP', 'ПАТАКА'),                                           ");
                sql.Append("                    ('480', 'MUR', 'Маврикийская рупия'),                               ");
                sql.Append("                    ('462', 'MVR', 'МАЛЬДИВСКАЯ РУФИЯ'),                                ");
                sql.Append("                    ('484', 'MXN', 'МЕКСИКАНСКОЕ ПЕСО'),                                ");
                sql.Append("                    ('458', 'MYR', 'МАЛАЙЗИЙСКИЙ РИНГГИТ'),                             ");
                sql.Append("                    ('516', 'NAD', 'Доллар Намибии'),                                   ");
                sql.Append("                    ('566', 'NGN', 'НАЙРА'),                                            ");
                sql.Append("                    ('558', 'NIO', 'Золотая кордоба'),                                  ");
                sql.Append("                    ('578', 'NOK', 'Норвежская крона'),                                 ");
                sql.Append("                    ('524', 'NPR', 'Непальская рупия'),                                 ");
                sql.Append("                    ('554', 'NZD', 'Новозеландский доллар'),                            ");
                sql.Append("                    ('512', 'OMR', 'Оманский риал'),                                    ");
                sql.Append("                    ('604', 'PEN', 'НОВЫЙ СОЛЬ'),                                       ");
                sql.Append("                    ('598', 'PGK', 'Кина'),                                             ");
                sql.Append("                    ('586', 'PKR', 'ПАКИСТАНСКАЯ РУПИЯ'),                               ");
                sql.Append("                    ('985', 'PLN', 'ЗЛОТЫЙ'),                                           ");
                sql.Append("                    ('634', 'QAR', 'КАТАРСКИЙ РИАЛ'),                                   ");
                sql.Append("                    ('946', 'RON', 'РУМЫНСКИЙ ЛЕЙ'),                                    ");
                sql.Append("                    ('941', 'RSD', 'СЕРБСКИЙ ДИНАР'),                                   ");
                sql.Append("                    ('690', 'SCR', 'Сейшельская рупия'),                                ");
                sql.Append("                    ('736', 'SDD', 'Суданский динар'),                                  ");
                sql.Append("                    ('752', 'SEK', 'Шведская крона'),                                   ");
                sql.Append("                    ('702', 'SGD', 'Сингапурский доллар'),                              ");
                sql.Append("                    ('705', 'SIT', 'Толар'),                                            ");
                sql.Append("                    ('703', 'SKK', 'Словацкая крона'),                                  ");
                sql.Append("                    ('760', 'SYP', 'Сирийский фунт'),                                   ");
                sql.Append("                    ('764', 'THB', 'Бат'),                                              ");
                sql.Append("                    ('972', 'TJS', 'СОМОНИ'),                                           ");
                sql.Append("                    ('795', 'TMM', 'Манат'),                                            ");
                sql.Append("                    ('934', 'TMT', 'НОВЫЙ ТУРКМЕНСКИЙ МАНАТ'),                          ");
                sql.Append("                    ('788', 'TND', 'Тунисский динар'),                                  ");
                sql.Append("                    ('626', 'TPE', 'Тиморское эскудо'),                                 ");
                sql.Append("                    ('949', 'TRY', 'ТУРЕЦКАЯ ЛИРА'),                                    ");
                sql.Append("                    ('834', 'TZS', 'ТАНЗАНИЙСКИЙ ШИЛЛИНГ'),                             ");
                sql.Append("                    ('980', 'UAH', 'Украинская гривна'),                                ");
                sql.Append("                    ('800', 'UGX', 'УГАНДИЙСКИЙ ШИЛЛИНГ'),                              ");
                sql.Append("                    ('860', 'UZS', 'Узбекский сум'),                                    ");
                sql.Append("                    ('704', 'VND', 'Донг'),                                             ");
                sql.Append("                    ('950', 'XAF', 'Франк КФА ВЕАС'),                                   ");
                sql.Append("                    ('959', 'XAU', 'Золото в гр.'),                                     ");
                sql.Append("                    ('960', 'XDR', 'СДР (специальные права заимствования)'),            ");
                sql.Append("                    ('710', 'ZAR', 'Рэнд'),                                             ");
                sql.Append("                    ('276', 'DEM', 'Немецкая марка Германия'),                          ");
                sql.Append("                    ('056', 'BEF', 'франк Бельгии'),                                    ");
                sql.Append("                    ('246', 'FIM', 'ФИНЛЯНДСКАЯ МАРКА'),                                ");
                sql.Append("                    ('250', 'FRF', 'ФРАНЦУЗСКИЕ ФРАНКИ'),                               ");
                sql.Append("                    ('380', 'ITL', 'ИТАЛЬЯНСКИЕ ЛИРЫ'),                                 ");
                sql.Append("                    ('040', 'ATS', 'АВСТРИЙСКИЕ ШИЛЛИНГИ'),                             ");
                sql.Append("                    ('528', 'NLG', 'НИДЕРЛАНДСКИЕ ГУЛЬДЕНЫ'),                           ");
                sql.Append("                    ('724', 'ESP', 'ИСПАНСКИЕ ПЕСЕТЫ');                                 ");

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
                sql.Append("    PRIMARY KEY(MaskTypeId AUTOINCREMENT)                                               ");
                sql.Append(" );                                                                                     ");

                sql.Append(" CREATE TABLE IF NOT EXISTS [Mask] (                                                    ");
                sql.Append("    MaskId           INTEGER        NOT NULL,                                           ");
                sql.Append("    MaskTypeId       INTEGER        NOT NULL                                            ");
                sql.Append("                                    REFERENCES [MaskType](MaskTypeId) ON DELETE CASCADE ");
                sql.Append("                                                                      ON UPDATE CASCADE,");
                sql.Append("    Content          VARCHAR(100)   NOT NULL,                                           ");
                sql.Append("    SequenceNumber   INTEGER        NOT NULL,                                           ");
                sql.Append("    PRIMARY KEY(MaskId AUTOINCREMENT)                                                   ");
                sql.Append(" );                                                                                     ");

                sql.Append(" CREATE VIEW IF NOT EXISTS [CompanyView]                                                ");
                sql.Append(" AS                                                                                     ");
                sql.Append(" SELECT                                                                                 ");
                sql.Append("    g.Name as GroupName,                                                                ");
                sql.Append("    c.CompanyId,                                                                        ");
                sql.Append("    c.Name as CompanyName,                                                              ");
                sql.Append("    c.Inn,                                                                              ");
                sql.Append("    m.Name as ManagerName                                                               ");
                sql.Append(" FROM [Company] as c                                                                    ");
                sql.Append(" INNER JOIN [Group] as g ON c.GroupId = g.GroupId                                       ");
                sql.Append(" INNER JOIN [Manager] as m ON c.ManagerId = m.ManagerId;                                ");

                using var connection = new SqliteConnection(dbConfig.Name);
                connection.Execute(sql.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
