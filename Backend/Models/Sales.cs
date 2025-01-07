public class Sales {
    public int Id {get;set;}

    public  int Customer_id {get;set;}
    public int  Product_id {get;set;}
    public int Total_amount {get;set;}
    public int Quantity{get;set;}

    public DateTime  Sale_date{get;set;}=DateTime.Now;
    public string Payment_method{get;set;}

}

public class SalesViewModel : Sales 
{
    public string CustomerName { get; set; } = string.Empty;
    public string ProductName { get; set; } = string.Empty;

}