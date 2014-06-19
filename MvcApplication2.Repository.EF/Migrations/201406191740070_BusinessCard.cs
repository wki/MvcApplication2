namespace MvcApplication2.Repository.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BusinessCard : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BusinessCardStates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EmployeeId = c.Int(nullable: false),
                        Name = c.String(),
                        Department = c.String(),
                        Title = c.String(),
                        Phone = c.String(),
                        EMail = c.String(),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.HistoryEntryStates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OccuredOn = c.DateTime(nullable: false),
                        Message = c.String(),
                        BusinessCardState_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BusinessCardStates", t => t.BusinessCardState_Id)
                .Index(t => t.BusinessCardState_Id);
            
            DropTable("dbo.CardRecords");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.CardRecords",
                c => new
                    {
                        CardId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Phone = c.String(),
                    })
                .PrimaryKey(t => t.CardId);
            
            DropForeignKey("dbo.HistoryEntryStates", "BusinessCardState_Id", "dbo.BusinessCardStates");
            DropIndex("dbo.HistoryEntryStates", new[] { "BusinessCardState_Id" });
            DropTable("dbo.HistoryEntryStates");
            DropTable("dbo.BusinessCardStates");
        }
    }
}
