using FluentMigrator;

namespace dotnet_practice.Migrations
{
    [Migration(20250102_001)] 
    public class CreateCustomerProductSakesTable  : Migration
    {
        public override void Up()
        {
            // Create the users table
            Create.Table("customer")
                .WithColumn("id").AsInt32().PrimaryKey().Identity() 
                .WithColumn("name").AsString(100).NotNullable()
                .WithColumn("phone").AsString(50).Nullable()
                .WithColumn("email").AsString(100).NotNullable().Unique()                  
                .WithColumn("city").AsString().NotNullable();  

            // create the Prduct table

            Create.Table("product")
                    .WithColumn("id").AsInt32().PrimaryKey().Identity()
                    .WithColumn("name").AsString(225).NotNullable()
                    .WithColumn("category").AsString(225).NotNullable()
                    .WithColumn("description").AsString(300).NotNullable()
                    .WithColumn("price").AsDecimal().NotNullable()
                    .WithColumn("stock_quantity").AsInt32().NotNullable();

           // create the sales Table 

            Create.Table("sales")
                  .WithColumn("id").AsInt32().PrimaryKey().Identity()
                  .WithColumn("customer_id").AsInt32().NotNullable()
                  .WithColumn("product_id").AsInt32().NotNullable()
                  .WithColumn("total_amoubt").AsDecimal().NotNullable()
                   .WithColumn("quantity").AsInt32().NotNullable()
                   .WithColumn("sale_date").AsDateTime().WithDefault(SystemMethods.CurrentUTCDateTime)
                    .WithColumn("payment_method").AsString().NotNullable();

            // foreign keys to sales table 

            Create.ForeignKey("fk_sales_customer")
                    .FromTable("sales").ForeignColumn("product_id")
                    .ToTable("product").PrimaryColumn("id");

             Create.ForeignKey("fk_sales_product")
                    .FromTable("sales").ForeignColumn("product_id")
                    .ToTable("product").PrimaryColumn("id");


        }
    
          public override void Down()
        {
            // Delete.Table("User");
        }
    }
}