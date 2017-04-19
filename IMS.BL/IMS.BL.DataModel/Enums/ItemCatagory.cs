namespace IMS.BL.DataModel
{
    public enum ItemCatagory
    {
        Alchol,
        Chilled,
        SoftDrink,
        Confectionery
    }

    public static class ItemCatagoryExtension
    {
        public static string StringRepresentation(this ItemCatagory catagory)
        {
            switch (catagory)
            {
                case ItemCatagory.Alchol:
                    return "Alchol";
                case ItemCatagory.Chilled:
                    return "Chilled";
                case ItemCatagory.Confectionery:
                    return "Confectionery";
                case ItemCatagory.SoftDrink:
                    return "Soft Drink";
                //some error occured
                default:
                    return null;
            }
        }
    }
}
