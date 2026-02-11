using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace SchoolManagementSystem
{
    public static class TemplateUI
        {
            public static DataGridView CreateStyledGrid()
            {
                return new DataGridView
                {
                    //ScrollBars = ScrollBars.Horizontal; // hide vertical scroll

                    AllowUserToAddRows = false,
                    AllowUserToDeleteRows = false,
                    AllowUserToResizeColumns = false,
                    AllowUserToResizeRows = false,
                    AllowUserToOrderColumns = false,
                    AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                    BackgroundColor = Color.White,
                    BorderStyle = BorderStyle.None,
                    CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
                    ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None,
                    ColumnHeadersHeight = 24,
                    ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing,
                    EnableHeadersVisualStyles = false,
                    ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
                    {
                        BackColor = Color.LightGray,
                        ForeColor = Color.Black,
                        Font = new Font("Segoe UI", 10, FontStyle.Bold),
                        SelectionBackColor = Color.LightGray, // Prevent header turning gray/blue
                        SelectionForeColor = Color.Black,
                    },
                    SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                    GridColor = SystemColors.MenuText,
                    Location = new Point(0, 0),
                    Name = "dgv",
                    ReadOnly = true,
                    RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single,
                    RowHeadersVisible = false,
                    RowHeadersWidth = 10,
                    ScrollBars = ScrollBars.Both,
                    //Size = new Size(898, 474),
                    TabIndex = 8,
                    TabStop = false,
                    //EnableHeadersVisualStyles = false; // Allow custom header style
                    //ColumnHeadersDefaultCellStyle.BackColor = Color.SteelBlue,
                    //ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                    DefaultCellStyle = new DataGridViewCellStyle
                    {

                        SelectionBackColor = Color.FromArgb(223, 241, 250), 
                        SelectionForeColor = Color.Black
                    },
                    RowTemplate = { Height = 30 },
                    Height = (30*15) +24,
                    Dock = DockStyle.Fill,

                };
            }
        }
}