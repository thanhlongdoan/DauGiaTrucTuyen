namespace DauGiaTrucTuyen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addCreateDate_for_table_User2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "CreateDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "CreateDate", c => c.DateTime(nullable: false));
        }
    }
}
