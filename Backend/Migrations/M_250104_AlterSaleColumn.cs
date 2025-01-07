using FluentMigrator;

namespace dotnet_practice.Migrations;

[Migration(202501041056)]
public class M_250104_AlterSaleColumn : Migration
{
    public override void Up()
    {
        Rename.Column("total_amoubt").OnTable("sales").To("total_amount");
    }
    public override void Down()
    {
        throw new NotImplementedException();
    }

}