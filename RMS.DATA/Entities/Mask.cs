namespace RMS.DATA.Entities
{
    /// <summary>
    /// сущность для хранения масок поиска и алализа данных выгрузок
    /// </summary>
    public class Mask
    {
        public int MaskId { get; set; } = 0;
        public int MaskTypeId { get; set; } = 0;
        public string Content { get; set; } = string.Empty;
    }
}
