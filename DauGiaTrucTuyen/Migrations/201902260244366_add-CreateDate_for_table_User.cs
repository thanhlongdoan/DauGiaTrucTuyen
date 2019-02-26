namespace DauGiaTrucTuyen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addCreateDate_for_table_User : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "CreateDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "CreateDate");
        }
    }
}
