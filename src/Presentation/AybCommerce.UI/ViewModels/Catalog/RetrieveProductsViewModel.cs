namespace AybCommerce.UI.ViewModels.Catalog
{
    public class RetrieveProductsViewModel
    {
        public int PageIndex { get; set; } = 0;

        public int PageSize { get; set; } = 10; //100 itemsOnPage

        public int? CategoryId { get; set; }
    }
}
