namespace DauGiaTrucTuyen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.MessageChat", newName: "Message_User_Chat");
            RenameTable(name: "dbo.UserChat", newName: "Users_Chat");
            AddColumn("dbo.Message_User_Chat", "Message", c => c.String());
            DropColumn("dbo.Message_User_Chat", "Msg");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Message_User_Chat", "Msg", c => c.String());
            DropColumn("dbo.Message_User_Chat", "Message");
            RenameTable(name: "dbo.Users_Chat", newName: "UserChat");
            RenameTable(name: "dbo.Message_User_Chat", newName: "MessageChat");
        }
    }
}
