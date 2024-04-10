using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Task_Manager_Beta.Migrations
{
    /// <inheritdoc />
    public partial class AddAllTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PROJECT",
                columns: table => new
                {
                    IDProject = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IDLeader = table.Column<int>(type: "int", nullable: true),
                    DayCreate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Hide = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PROJECT__B0529955CFDF06BE", x => x.IDProject);
                });

            migrationBuilder.CreateTable(
                name: "STATUS",
                columns: table => new
                {
                    IDStatus = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__STATUS__8DA245106739C773", x => x.IDStatus);
                });

            migrationBuilder.CreateTable(
                name: "USER",
                columns: table => new
                {
                    IDUser = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Email = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Password = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    Hide = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__USER__EAE6D9DF10325B4D", x => x.IDUser);
                });

            migrationBuilder.CreateTable(
                name: "TASK",
                columns: table => new
                {
                    IDTask = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IDProject = table.Column<int>(type: "int", nullable: false),
                    IDStatus = table.Column<int>(type: "int", nullable: false),
                    TaskName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DayCreate = table.Column<DateTime>(type: "datetime", nullable: true),
                    DayStart = table.Column<DateTime>(type: "datetime", nullable: true),
                    Deadline = table.Column<DateTime>(type: "datetime", nullable: true),
                    Hide = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TASK__BCC3A1F92D2AF717", x => x.IDTask);
                    table.ForeignKey(
                        name: "FK__TASK__IDProject__5DCAEF64",
                        column: x => x.IDProject,
                        principalTable: "PROJECT",
                        principalColumn: "IDProject");
                    table.ForeignKey(
                        name: "FK__TASK__IDStatus__5EBF139D",
                        column: x => x.IDStatus,
                        principalTable: "STATUS",
                        principalColumn: "IDStatus");
                });

            migrationBuilder.CreateTable(
                name: "TEMPLATE",
                columns: table => new
                {
                    IDTemplate = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IDProject = table.Column<int>(type: "int", nullable: false),
                    IDStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TEMPLATE__AB4388EFDD927C1E", x => x.IDTemplate);
                    table.ForeignKey(
                        name: "FK__TEMPLATE__IDProj__693CA210",
                        column: x => x.IDProject,
                        principalTable: "PROJECT",
                        principalColumn: "IDProject");
                    table.ForeignKey(
                        name: "FK__TEMPLATE__IDStat__6A30C649",
                        column: x => x.IDStatus,
                        principalTable: "STATUS",
                        principalColumn: "IDStatus");
                });

            migrationBuilder.CreateTable(
                name: "WORKFLOW",
                columns: table => new
                {
                    IDWorkflow = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IDStatus = table.Column<int>(type: "int", nullable: false),
                    Transition = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__WORKFLOW__7D45E40AA14ADF65", x => x.IDWorkflow);
                    table.ForeignKey(
                        name: "FK__WORKFLOW__IDStat__66603565",
                        column: x => x.IDStatus,
                        principalTable: "STATUS",
                        principalColumn: "IDStatus");
                });

            migrationBuilder.CreateTable(
                name: "MEMBER",
                columns: table => new
                {
                    IDMember = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IDUser = table.Column<int>(type: "int", nullable: false),
                    IDProject = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__MEMBER__7EB75A63D92FB36D", x => x.IDMember);
                    table.ForeignKey(
                        name: "FK__MEMBER__IDProjec__619B8048",
                        column: x => x.IDProject,
                        principalTable: "PROJECT",
                        principalColumn: "IDProject");
                    table.ForeignKey(
                        name: "FK__MEMBER__IDUser__60A75C0F",
                        column: x => x.IDUser,
                        principalTable: "USER",
                        principalColumn: "IDUser");
                });

            migrationBuilder.CreateTable(
                name: "PERMISSSION",
                columns: table => new
                {
                    IDPermission = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IDUser = table.Column<int>(type: "int", nullable: false),
                    IDProject = table.Column<int>(type: "int", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Object = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Privilege = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PERMISSS__0553C49AB4E77293", x => x.IDPermission);
                    table.ForeignKey(
                        name: "FK__PERMISSSI__IDPro__656C112C",
                        column: x => x.IDProject,
                        principalTable: "PROJECT",
                        principalColumn: "IDProject");
                    table.ForeignKey(
                        name: "FK__PERMISSSI__IDUse__6477ECF3",
                        column: x => x.IDUser,
                        principalTable: "USER",
                        principalColumn: "IDUser");
                });

            migrationBuilder.CreateTable(
                name: "ASSIGNMENT",
                columns: table => new
                {
                    IDUser = table.Column<int>(type: "int", nullable: false),
                    IDTask = table.Column<int>(type: "int", nullable: false),
                    IDAssignment = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ASSIGNME__612AE3C08133FCF6", x => new { x.IDUser, x.IDTask });
                    table.ForeignKey(
                        name: "FK__ASSIGNMEN__IDTas__6383C8BA",
                        column: x => x.IDTask,
                        principalTable: "TASK",
                        principalColumn: "IDTask");
                    table.ForeignKey(
                        name: "FK__ASSIGNMEN__IDUse__628FA481",
                        column: x => x.IDUser,
                        principalTable: "USER",
                        principalColumn: "IDUser");
                });

            migrationBuilder.CreateTable(
                name: "TASKDETAILS",
                columns: table => new
                {
                    IDTask = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Attachments = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TASKDETA__BCC3A1F9F0F81026", x => x.IDTask);
                    table.ForeignKey(
                        name: "FK__TASKDETAI__IDTas__5FB337D6",
                        column: x => x.IDTask,
                        principalTable: "TASK",
                        principalColumn: "IDTask");
                });

            migrationBuilder.CreateTable(
                name: "CONDITION",
                columns: table => new
                {
                    IDCondition = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IDPermission = table.Column<int>(type: "int", nullable: false),
                    IDWorkflow = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CONDITIO__452B4D93285C0699", x => x.IDCondition);
                    table.ForeignKey(
                        name: "FK__CONDITION__IDPer__6754599E",
                        column: x => x.IDPermission,
                        principalTable: "PERMISSSION",
                        principalColumn: "IDPermission");
                    table.ForeignKey(
                        name: "FK__CONDITION__IDWor__68487DD7",
                        column: x => x.IDWorkflow,
                        principalTable: "WORKFLOW",
                        principalColumn: "IDWorkflow");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ASSIGNMENT_IDTask",
                table: "ASSIGNMENT",
                column: "IDTask");

            migrationBuilder.CreateIndex(
                name: "IX_CONDITION_IDPermission",
                table: "CONDITION",
                column: "IDPermission");

            migrationBuilder.CreateIndex(
                name: "IX_CONDITION_IDWorkflow",
                table: "CONDITION",
                column: "IDWorkflow");

            migrationBuilder.CreateIndex(
                name: "IX_MEMBER_IDProject",
                table: "MEMBER",
                column: "IDProject");

            migrationBuilder.CreateIndex(
                name: "IX_MEMBER_IDUser",
                table: "MEMBER",
                column: "IDUser");

            migrationBuilder.CreateIndex(
                name: "IX_PERMISSSION_IDProject",
                table: "PERMISSSION",
                column: "IDProject");

            migrationBuilder.CreateIndex(
                name: "IX_PERMISSSION_IDUser",
                table: "PERMISSSION",
                column: "IDUser");

            migrationBuilder.CreateIndex(
                name: "UQ__STATUS__05E7698A81D61DE0",
                table: "STATUS",
                column: "StatusName",
                unique: true,
                filter: "[StatusName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TASK_IDProject",
                table: "TASK",
                column: "IDProject");

            migrationBuilder.CreateIndex(
                name: "IX_TASK_IDStatus",
                table: "TASK",
                column: "IDStatus");

            migrationBuilder.CreateIndex(
                name: "IX_TEMPLATE_IDProject",
                table: "TEMPLATE",
                column: "IDProject");

            migrationBuilder.CreateIndex(
                name: "IX_TEMPLATE_IDStatus",
                table: "TEMPLATE",
                column: "IDStatus");

            migrationBuilder.CreateIndex(
                name: "IX_WORKFLOW_IDStatus",
                table: "WORKFLOW",
                column: "IDStatus");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ASSIGNMENT");

            migrationBuilder.DropTable(
                name: "CONDITION");

            migrationBuilder.DropTable(
                name: "MEMBER");

            migrationBuilder.DropTable(
                name: "TASKDETAILS");

            migrationBuilder.DropTable(
                name: "TEMPLATE");

            migrationBuilder.DropTable(
                name: "PERMISSSION");

            migrationBuilder.DropTable(
                name: "WORKFLOW");

            migrationBuilder.DropTable(
                name: "TASK");

            migrationBuilder.DropTable(
                name: "USER");

            migrationBuilder.DropTable(
                name: "PROJECT");

            migrationBuilder.DropTable(
                name: "STATUS");
        }
    }
}
