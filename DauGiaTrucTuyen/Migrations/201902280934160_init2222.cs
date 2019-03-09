namespace DauGiaTrucTuyen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init2222 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Users", "BlockUser");
            DropColumn("dbo.Users", "BlockDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "BlockDate", c => c.DateTime());
            AddColumn("dbo.Users", "BlockUser", c => c.Boolean());
        }
    }
}
