using System.ComponentModel;

namespace SchoolManagementSystem
{
    partial class dashboardControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            panelDashboard = new Panel();
            panelResult = new Panel();
            panel5 = new Panel();
            panelResultDaily = new Panel();
            chartResultMonthly = new System.Windows.Forms.DataVisualization.Charting.Chart();
            panel1 = new Panel();
            panel4 = new Panel();
            panelResultSum = new Panel();
            panel6 = new Panel();
            panel7 = new Panel();
            chartFemaleMale = new System.Windows.Forms.DataVisualization.Charting.Chart();
            panel8 = new Panel();
            Total = new Label();
            label11 = new Label();
            lbStopStudent = new Label();
            lbTotalStudent = new Label();
            label12 = new Label();
            lbNewStudent = new Label();
            panel3 = new Panel();
            label10 = new Label();
            label18 = new Label();
            panel2 = new Panel();
            panel10 = new Panel();
            panel11 = new Panel();
            chartPaidIsActive = new System.Windows.Forms.DataVisualization.Charting.Chart();
            panel12 = new Panel();
            label21 = new Label();
            label19 = new Label();
            label24 = new Label();
            lbDPaid2 = new Label();
            lbDTotalPaid = new Label();
            lbActive = new Label();
            panel9 = new Panel();
            label27 = new Label();
            label15 = new Label();
            label9 = new Label();
            label4 = new Label();
            label6 = new Label();
            label13 = new Label();
            panelInformation = new Panel();
            panelInforamtionGeneral = new Panel();
            label14 = new Label();
            lbNumberPay = new Label();
            panelInformationGtrade = new Panel();
            lbNumberGrades = new Label();
            label3 = new Label();
            panelInforamtionTeacher = new Panel();
            lbNumberTeachers = new Label();
            label2 = new Label();
            panelInformationStudent = new Panel();
            lbNumberStudents = new Label();
            label1 = new Label();
            lbTotalStudentFemale = new Label();
            lbTotalStudentMale = new Label();
            lbDPaid1 = new Label();
            panelDashboard.SuspendLayout();
            panelResult.SuspendLayout();
            panel5.SuspendLayout();
            panelResultDaily.SuspendLayout();
            ((ISupportInitialize)chartResultMonthly).BeginInit();
            panel1.SuspendLayout();
            panel4.SuspendLayout();
            panelResultSum.SuspendLayout();
            panel6.SuspendLayout();
            panel7.SuspendLayout();
            ((ISupportInitialize)chartFemaleMale).BeginInit();
            panel8.SuspendLayout();
            panel3.SuspendLayout();
            panel2.SuspendLayout();
            panel10.SuspendLayout();
            panel11.SuspendLayout();
            ((ISupportInitialize)chartPaidIsActive).BeginInit();
            panel12.SuspendLayout();
            panel9.SuspendLayout();
            panelInformation.SuspendLayout();
            panelInforamtionGeneral.SuspendLayout();
            panelInformationGtrade.SuspendLayout();
            panelInforamtionTeacher.SuspendLayout();
            panelInformationStudent.SuspendLayout();
            SuspendLayout();
            // 
            // panelDashboard
            // 
            panelDashboard.BackColor = Color.White;
            panelDashboard.Controls.Add(panelResult);
            panelDashboard.Controls.Add(panelInformation);
            panelDashboard.Dock = DockStyle.Fill;
            panelDashboard.Location = new Point(0, 0);
            panelDashboard.Margin = new Padding(3, 2, 3, 2);
            panelDashboard.Name = "panelDashboard";
            panelDashboard.Size = new Size(982, 663);
            panelDashboard.TabIndex = 3;
            // 
            // panelResult
            // 
            panelResult.BackColor = Color.WhiteSmoke;
            panelResult.Controls.Add(panel5);
            panelResult.Controls.Add(panel1);
            panelResult.Dock = DockStyle.Fill;
            panelResult.Location = new Point(0, 109);
            panelResult.Margin = new Padding(3, 2, 3, 2);
            panelResult.Name = "panelResult";
            panelResult.Size = new Size(982, 554);
            panelResult.TabIndex = 1;
            // 
            // panel5
            // 
            panel5.Controls.Add(panelResultDaily);
            panel5.Dock = DockStyle.Fill;
            panel5.Location = new Point(0, 229);
            panel5.Margin = new Padding(0);
            panel5.Name = "panel5";
            panel5.Padding = new Padding(23, 10, 23, 10);
            panel5.Size = new Size(982, 325);
            panel5.TabIndex = 4;
            // 
            // panelResultDaily
            // 
            panelResultDaily.BackColor = Color.White;
            panelResultDaily.Controls.Add(chartResultMonthly);
            panelResultDaily.Dock = DockStyle.Fill;
            panelResultDaily.Location = new Point(23, 10);
            panelResultDaily.Margin = new Padding(3, 2, 3, 2);
            panelResultDaily.Name = "panelResultDaily";
            panelResultDaily.Size = new Size(936, 305);
            panelResultDaily.TabIndex = 1;
            // 
            // chartResultMonthly
            // 
            chartArea1.Name = "ChartArea1";
            chartResultMonthly.ChartAreas.Add(chartArea1);
            chartResultMonthly.Dock = DockStyle.Fill;
            chartResultMonthly.Location = new Point(0, 0);
            chartResultMonthly.Name = "chartResultMonthly";
            series1.ChartArea = "ChartArea1";
            series1.Name = "Series1";
            chartResultMonthly.Series.Add(series1);
            chartResultMonthly.Size = new Size(936, 305);
            chartResultMonthly.TabIndex = 0;
            chartResultMonthly.Text = "chart1";
            // 
            // panel1
            // 
            panel1.Controls.Add(panel4);
            panel1.Controls.Add(label9);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(label6);
            panel1.Controls.Add(label13);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(982, 229);
            panel1.TabIndex = 3;
            // 
            // panel4
            // 
            panel4.Controls.Add(panelResultSum);
            panel4.Controls.Add(panel2);
            panel4.Dock = DockStyle.Bottom;
            panel4.Location = new Point(0, 41);
            panel4.Name = "panel4";
            panel4.Size = new Size(982, 188);
            panel4.TabIndex = 3;
            // 
            // panelResultSum
            // 
            panelResultSum.BackColor = Color.White;
            panelResultSum.Controls.Add(panel6);
            panelResultSum.Controls.Add(panel3);
            panelResultSum.Controls.Add(label18);
            panelResultSum.ForeColor = SystemColors.ActiveCaptionText;
            panelResultSum.Location = new Point(23, 18);
            panelResultSum.Margin = new Padding(3, 2, 3, 2);
            panelResultSum.Name = "panelResultSum";
            panelResultSum.Size = new Size(450, 150);
            panelResultSum.TabIndex = 1;
            // 
            // panel6
            // 
            panel6.Controls.Add(panel7);
            panel6.Controls.Add(panel8);
            panel6.Dock = DockStyle.Fill;
            panel6.Location = new Point(0, 31);
            panel6.Name = "panel6";
            panel6.Size = new Size(450, 119);
            panel6.TabIndex = 4;
            // 
            // panel7
            // 
            panel7.Controls.Add(chartFemaleMale);
            panel7.Dock = DockStyle.Fill;
            panel7.Location = new Point(0, 0);
            panel7.Name = "panel7";
            panel7.Size = new Size(303, 119);
            panel7.TabIndex = 2;
            // 
            // chartFemaleMale
            // 
            chartArea2.Name = "ChartArea1";
            chartFemaleMale.ChartAreas.Add(chartArea2);
            chartFemaleMale.Dock = DockStyle.Fill;
            chartFemaleMale.Location = new Point(0, 0);
            chartFemaleMale.Name = "chartFemaleMale";
            series2.ChartArea = "ChartArea1";
            series2.Name = "Series1";
            chartFemaleMale.Series.Add(series2);
            chartFemaleMale.Size = new Size(303, 119);
            chartFemaleMale.TabIndex = 3;
            // 
            // panel8
            // 
            panel8.Controls.Add(Total);
            panel8.Controls.Add(label11);
            panel8.Controls.Add(lbStopStudent);
            panel8.Controls.Add(lbTotalStudent);
            panel8.Controls.Add(label12);
            panel8.Controls.Add(lbNewStudent);
            panel8.Dock = DockStyle.Right;
            panel8.Location = new Point(303, 0);
            panel8.Name = "panel8";
            panel8.Size = new Size(147, 119);
            panel8.TabIndex = 1;
            // 
            // Total
            // 
            Total.AutoSize = true;
            Total.Font = new Font("Poppins", 9F);
            Total.Location = new Point(20, 84);
            Total.Name = "Total";
            Total.Size = new Size(39, 22);
            Total.TabIndex = 2;
            Total.Text = "Total";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Poppins", 9F);
            label11.ForeColor = Color.Green;
            label11.Location = new Point(20, 13);
            label11.Name = "label11";
            label11.Size = new Size(35, 22);
            label11.TabIndex = 1;
            label11.Text = "New";
            // 
            // lbStopStudent
            // 
            lbStopStudent.AutoSize = true;
            lbStopStudent.Font = new Font("Poppins", 9F);
            lbStopStudent.ForeColor = Color.Red;
            lbStopStudent.Location = new Point(70, 50);
            lbStopStudent.Name = "lbStopStudent";
            lbStopStudent.Size = new Size(18, 22);
            lbStopStudent.TabIndex = 1;
            lbStopStudent.Text = "0";
            // 
            // lbTotalStudent
            // 
            lbTotalStudent.AutoSize = true;
            lbTotalStudent.Font = new Font("Poppins", 9F);
            lbTotalStudent.Location = new Point(70, 84);
            lbTotalStudent.Name = "lbTotalStudent";
            lbTotalStudent.Size = new Size(18, 22);
            lbTotalStudent.TabIndex = 2;
            lbTotalStudent.Text = "0";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new Font("Poppins", 9F);
            label12.ForeColor = Color.Red;
            label12.Location = new Point(20, 50);
            label12.Name = "label12";
            label12.Size = new Size(37, 22);
            label12.TabIndex = 1;
            label12.Text = "Stop";
            // 
            // lbNewStudent
            // 
            lbNewStudent.AutoSize = true;
            lbNewStudent.Font = new Font("Poppins", 9F);
            lbNewStudent.ForeColor = Color.Green;
            lbNewStudent.Location = new Point(70, 13);
            lbNewStudent.Name = "lbNewStudent";
            lbNewStudent.Size = new Size(18, 22);
            lbNewStudent.TabIndex = 1;
            lbNewStudent.Text = "0";
            // 
            // panel3
            // 
            panel3.Controls.Add(label10);
            panel3.Dock = DockStyle.Top;
            panel3.Location = new Point(0, 0);
            panel3.Name = "panel3";
            panel3.Size = new Size(450, 31);
            panel3.TabIndex = 3;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Poppins", 9F);
            label10.Location = new Point(3, 6);
            label10.Name = "label10";
            label10.Size = new Size(62, 22);
            label10.TabIndex = 0;
            label10.Text = "Students";
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Location = new Point(681, 204);
            label18.Name = "label18";
            label18.Size = new Size(44, 15);
            label18.TabIndex = 2;
            label18.Text = "label13";
            // 
            // panel2
            // 
            panel2.BackColor = Color.White;
            panel2.Controls.Add(panel10);
            panel2.Controls.Add(panel9);
            panel2.Controls.Add(label15);
            panel2.ForeColor = SystemColors.ActiveCaptionText;
            panel2.Location = new Point(506, 18);
            panel2.Margin = new Padding(3, 2, 3, 2);
            panel2.Name = "panel2";
            panel2.Size = new Size(450, 150);
            panel2.TabIndex = 1;
            // 
            // panel10
            // 
            panel10.Controls.Add(panel11);
            panel10.Controls.Add(panel12);
            panel10.Dock = DockStyle.Fill;
            panel10.Location = new Point(0, 31);
            panel10.Name = "panel10";
            panel10.Size = new Size(450, 119);
            panel10.TabIndex = 4;
            // 
            // panel11
            // 
            panel11.Controls.Add(chartPaidIsActive);
            panel11.Dock = DockStyle.Fill;
            panel11.Location = new Point(0, 0);
            panel11.Name = "panel11";
            panel11.Size = new Size(319, 119);
            panel11.TabIndex = 2;
            // 
            // chartPaidIsActive
            // 
            chartArea3.Name = "ChartArea1";
            chartPaidIsActive.ChartAreas.Add(chartArea3);
            chartPaidIsActive.Dock = DockStyle.Fill;
            chartPaidIsActive.Location = new Point(0, 0);
            chartPaidIsActive.Name = "chartPaidIsActive";
            series3.ChartArea = "ChartArea1";
            series3.Name = "Series1";
            chartPaidIsActive.Series.Add(series3);
            chartPaidIsActive.Size = new Size(319, 119);
            chartPaidIsActive.TabIndex = 0;
            chartPaidIsActive.Text = "chart1";
            // 
            // panel12
            // 
            panel12.Controls.Add(label21);
            panel12.Controls.Add(label19);
            panel12.Controls.Add(label24);
            panel12.Controls.Add(lbDPaid2);
            panel12.Controls.Add(lbDTotalPaid);
            panel12.Controls.Add(lbActive);
            panel12.Dock = DockStyle.Right;
            panel12.Location = new Point(319, 0);
            panel12.Name = "panel12";
            panel12.Size = new Size(131, 119);
            panel12.TabIndex = 1;
            // 
            // label21
            // 
            label21.AutoSize = true;
            label21.Font = new Font("Poppins", 9F);
            label21.ForeColor = Color.Green;
            label21.Location = new Point(6, 13);
            label21.Name = "label21";
            label21.Size = new Size(36, 22);
            label21.TabIndex = 1;
            label21.Text = "Paid";
            // 
            // label19
            // 
            label19.AutoSize = true;
            label19.Font = new Font("Poppins", 9F);
            label19.Location = new Point(6, 84);
            label19.Name = "label19";
            label19.Size = new Size(39, 22);
            label19.TabIndex = 2;
            label19.Text = "Total";
            // 
            // label24
            // 
            label24.AutoSize = true;
            label24.Font = new Font("Poppins", 9F);
            label24.ForeColor = Color.DodgerBlue;
            label24.Location = new Point(6, 50);
            label24.Name = "label24";
            label24.Size = new Size(46, 22);
            label24.TabIndex = 1;
            label24.Text = "Active";
            // 
            // lbDPaid2
            // 
            lbDPaid2.AutoSize = true;
            lbDPaid2.Font = new Font("Poppins", 9F);
            lbDPaid2.ForeColor = Color.Green;
            lbDPaid2.Location = new Point(74, 13);
            lbDPaid2.Name = "lbDPaid2";
            lbDPaid2.Size = new Size(18, 22);
            lbDPaid2.TabIndex = 1;
            lbDPaid2.Text = "0";
            // 
            // lbDTotalPaid
            // 
            lbDTotalPaid.AutoSize = true;
            lbDTotalPaid.Font = new Font("Poppins", 9F);
            lbDTotalPaid.Location = new Point(74, 84);
            lbDTotalPaid.Name = "lbDTotalPaid";
            lbDTotalPaid.Size = new Size(18, 22);
            lbDTotalPaid.TabIndex = 2;
            lbDTotalPaid.Text = "0";
            // 
            // lbActive
            // 
            lbActive.AutoSize = true;
            lbActive.Font = new Font("Poppins", 9F);
            lbActive.ForeColor = Color.DodgerBlue;
            lbActive.Location = new Point(74, 50);
            lbActive.Name = "lbActive";
            lbActive.Size = new Size(18, 22);
            lbActive.TabIndex = 1;
            lbActive.Text = "0";
            // 
            // panel9
            // 
            panel9.Controls.Add(label27);
            panel9.Dock = DockStyle.Top;
            panel9.Location = new Point(0, 0);
            panel9.Name = "panel9";
            panel9.Size = new Size(450, 31);
            panel9.TabIndex = 3;
            // 
            // label27
            // 
            label27.AutoSize = true;
            label27.Font = new Font("Poppins", 9F);
            label27.Location = new Point(3, 6);
            label27.Name = "label27";
            label27.Size = new Size(63, 22);
            label27.TabIndex = 0;
            label27.Text = "Payment";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new Point(681, 204);
            label15.Name = "label15";
            label15.Size = new Size(44, 15);
            label15.TabIndex = 2;
            label15.Text = "label13";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Poppins", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label9.Location = new Point(21, 14);
            label9.Name = "label9";
            label9.Size = new Size(93, 22);
            label9.TabIndex = 0;
            label9.Text = "Result Sumery";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Poppins", 9F);
            label4.Location = new Point(136, 15);
            label4.Name = "label4";
            label4.Size = new Size(53, 22);
            label4.TabIndex = 2;
            label4.Text = "Female";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Poppins", 9F);
            label6.Location = new Point(219, 15);
            label6.Name = "label6";
            label6.Size = new Size(38, 22);
            label6.TabIndex = 2;
            label6.Text = "Male";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Font = new Font("Poppins", 9F);
            label13.Location = new Point(290, 15);
            label13.Name = "label13";
            label13.Size = new Size(39, 22);
            label13.TabIndex = 2;
            label13.Text = "Total";
            // 
            // panelInformation
            // 
            panelInformation.BackColor = Color.WhiteSmoke;
            panelInformation.Controls.Add(panelInforamtionGeneral);
            panelInformation.Controls.Add(panelInformationGtrade);
            panelInformation.Controls.Add(panelInforamtionTeacher);
            panelInformation.Controls.Add(panelInformationStudent);
            panelInformation.Dock = DockStyle.Top;
            panelInformation.Location = new Point(0, 0);
            panelInformation.Margin = new Padding(3, 2, 3, 2);
            panelInformation.Name = "panelInformation";
            panelInformation.Padding = new Padding(20, 10, 20, 10);
            panelInformation.Size = new Size(982, 109);
            panelInformation.TabIndex = 0;
            // 
            // panelInforamtionGeneral
            // 
            panelInforamtionGeneral.BackColor = Color.White;
            panelInforamtionGeneral.Controls.Add(label14);
            panelInforamtionGeneral.Controls.Add(lbNumberPay);
            panelInforamtionGeneral.Location = new Point(23, 12);
            panelInforamtionGeneral.Margin = new Padding(3, 2, 3, 2);
            panelInforamtionGeneral.Name = "panelInforamtionGeneral";
            panelInforamtionGeneral.Size = new Size(200, 80);
            panelInforamtionGeneral.TabIndex = 0;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Font = new Font("Poppins", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label14.Location = new Point(2, 61);
            label14.Name = "label14";
            label14.Size = new Size(36, 22);
            label14.TabIndex = 0;
            label14.Text = "Paid";
            // 
            // lbNumberPay
            // 
            lbNumberPay.AutoSize = true;
            lbNumberPay.Font = new Font("Poppins", 16.2F);
            lbNumberPay.Location = new Point(3, 26);
            lbNumberPay.Name = "lbNumberPay";
            lbNumberPay.Size = new Size(31, 39);
            lbNumberPay.TabIndex = 1;
            lbNumberPay.Text = "0";
            // 
            // panelInformationGtrade
            // 
            panelInformationGtrade.BackColor = Color.White;
            panelInformationGtrade.Controls.Add(lbNumberGrades);
            panelInformationGtrade.Controls.Add(label3);
            panelInformationGtrade.Location = new Point(520, 12);
            panelInformationGtrade.Margin = new Padding(3, 2, 3, 2);
            panelInformationGtrade.Name = "panelInformationGtrade";
            panelInformationGtrade.Size = new Size(200, 80);
            panelInformationGtrade.TabIndex = 0;
            // 
            // lbNumberGrades
            // 
            lbNumberGrades.AutoSize = true;
            lbNumberGrades.Font = new Font("Poppins", 16.2F);
            lbNumberGrades.Location = new Point(3, 26);
            lbNumberGrades.Name = "lbNumberGrades";
            lbNumberGrades.Size = new Size(31, 39);
            lbNumberGrades.TabIndex = 1;
            lbNumberGrades.Text = "0";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Poppins", 9F);
            label3.Location = new Point(3, 61);
            label3.Name = "label3";
            label3.Size = new Size(55, 22);
            label3.TabIndex = 0;
            label3.Text = "Classes";
            // 
            // panelInforamtionTeacher
            // 
            panelInforamtionTeacher.BackColor = Color.White;
            panelInforamtionTeacher.Controls.Add(lbNumberTeachers);
            panelInforamtionTeacher.Controls.Add(label2);
            panelInforamtionTeacher.Location = new Point(759, 12);
            panelInforamtionTeacher.Margin = new Padding(3, 2, 3, 2);
            panelInforamtionTeacher.Name = "panelInforamtionTeacher";
            panelInforamtionTeacher.Size = new Size(200, 80);
            panelInforamtionTeacher.TabIndex = 0;
            // 
            // lbNumberTeachers
            // 
            lbNumberTeachers.AutoSize = true;
            lbNumberTeachers.Font = new Font("Poppins", 16.2F);
            lbNumberTeachers.Location = new Point(3, 26);
            lbNumberTeachers.Name = "lbNumberTeachers";
            lbNumberTeachers.Size = new Size(103, 39);
            lbNumberTeachers.TabIndex = 1;
            lbNumberTeachers.Text = "Unkonw";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Poppins", 9F);
            label2.Location = new Point(3, 61);
            label2.Name = "label2";
            label2.Size = new Size(63, 22);
            label2.TabIndex = 0;
            label2.Text = "Teachers";
            // 
            // panelInformationStudent
            // 
            panelInformationStudent.BackColor = Color.White;
            panelInformationStudent.Controls.Add(lbNumberStudents);
            panelInformationStudent.Controls.Add(label1);
            panelInformationStudent.Location = new Point(267, 12);
            panelInformationStudent.Margin = new Padding(3, 2, 3, 2);
            panelInformationStudent.Name = "panelInformationStudent";
            panelInformationStudent.Size = new Size(200, 80);
            panelInformationStudent.TabIndex = 0;
            // 
            // lbNumberStudents
            // 
            lbNumberStudents.AutoSize = true;
            lbNumberStudents.Font = new Font("Poppins", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbNumberStudents.Location = new Point(3, 26);
            lbNumberStudents.Name = "lbNumberStudents";
            lbNumberStudents.Size = new Size(31, 39);
            lbNumberStudents.TabIndex = 1;
            lbNumberStudents.Text = "0";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Poppins", 9F);
            label1.Location = new Point(3, 61);
            label1.Name = "label1";
            label1.Size = new Size(62, 22);
            label1.TabIndex = 0;
            label1.Text = "Students";
            // 
            // lbTotalStudentFemale
            // 
            lbTotalStudentFemale.AutoSize = true;
            lbTotalStudentFemale.BackColor = Color.Transparent;
            lbTotalStudentFemale.Font = new Font("Poppins", 9F);
            lbTotalStudentFemale.ForeColor = SystemColors.ActiveCaptionText;
            lbTotalStudentFemale.Location = new Point(9, 0);
            lbTotalStudentFemale.Name = "lbTotalStudentFemale";
            lbTotalStudentFemale.Size = new Size(34, 22);
            lbTotalStudentFemale.TabIndex = 2;
            lbTotalStudentFemale.Text = "000";
            // 
            // lbTotalStudentMale
            // 
            lbTotalStudentMale.AutoSize = true;
            lbTotalStudentMale.BackColor = Color.Transparent;
            lbTotalStudentMale.Font = new Font("Poppins", 9F);
            lbTotalStudentMale.ForeColor = SystemColors.ActiveCaptionText;
            lbTotalStudentMale.Location = new Point(9, 0);
            lbTotalStudentMale.Name = "lbTotalStudentMale";
            lbTotalStudentMale.Size = new Size(34, 22);
            lbTotalStudentMale.TabIndex = 2;
            lbTotalStudentMale.Text = "000";
            // 
            // lbDPaid1
            // 
            lbDPaid1.AutoSize = true;
            lbDPaid1.BackColor = Color.Transparent;
            lbDPaid1.Font = new Font("Poppins", 9F);
            lbDPaid1.ForeColor = SystemColors.ActiveCaptionText;
            lbDPaid1.Location = new Point(141, 27);
            lbDPaid1.Name = "lbDPaid1";
            lbDPaid1.Size = new Size(34, 22);
            lbDPaid1.TabIndex = 2;
            lbDPaid1.Text = "000";
            // 
            // dashboardControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Controls.Add(panelDashboard);
            Margin = new Padding(3, 2, 3, 2);
            Name = "dashboardControl";
            Size = new Size(982, 663);
            panelDashboard.ResumeLayout(false);
            panelResult.ResumeLayout(false);
            panel5.ResumeLayout(false);
            panelResultDaily.ResumeLayout(false);
            ((ISupportInitialize)chartResultMonthly).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel4.ResumeLayout(false);
            panelResultSum.ResumeLayout(false);
            panelResultSum.PerformLayout();
            panel6.ResumeLayout(false);
            panel7.ResumeLayout(false);
            ((ISupportInitialize)chartFemaleMale).EndInit();
            panel8.ResumeLayout(false);
            panel8.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel10.ResumeLayout(false);
            panel11.ResumeLayout(false);
            ((ISupportInitialize)chartPaidIsActive).EndInit();
            panel12.ResumeLayout(false);
            panel12.PerformLayout();
            panel9.ResumeLayout(false);
            panel9.PerformLayout();
            panelInformation.ResumeLayout(false);
            panelInforamtionGeneral.ResumeLayout(false);
            panelInforamtionGeneral.PerformLayout();
            panelInformationGtrade.ResumeLayout(false);
            panelInformationGtrade.PerformLayout();
            panelInforamtionTeacher.ResumeLayout(false);
            panelInforamtionTeacher.PerformLayout();
            panelInformationStudent.ResumeLayout(false);
            panelInformationStudent.PerformLayout();
            ResumeLayout(false);
        }

        private Panel panelDashboard;
        private Panel panelResult;
        private Krypton.Toolkit.KryptonLabel lTotalMonth3;
        private Krypton.Toolkit.KryptonLabel lTotalMonth2;
        private Krypton.Toolkit.KryptonLabel lTotalMonth1;
        private Krypton.Toolkit.KryptonLabel lTotalMonth0;
        private Krypton.Toolkit.KryptonLabel lMaleMonth3;
        private Krypton.Toolkit.KryptonLabel lMaleMonth2;
        private Krypton.Toolkit.KryptonLabel lMaleMonth1;
        private Krypton.Toolkit.KryptonLabel lMaleMonth0;
        private Krypton.Toolkit.KryptonLabel lFemaleMonth3;
        private Krypton.Toolkit.KryptonLabel lFemaleMonth2;
        private Krypton.Toolkit.KryptonLabel lFemaleMonth1;
        private Krypton.Toolkit.KryptonLabel lFemaleMonth0;
        private Krypton.Toolkit.KryptonPanel pMaleMonth2;
        private Krypton.Toolkit.KryptonPanel pMaleMonth3;
        private Krypton.Toolkit.KryptonPanel pTotalMonth3;
        private Krypton.Toolkit.KryptonPanel pTotalMonth0;
        private Krypton.Toolkit.KryptonPanel pMaleMonth0;
        private Krypton.Toolkit.KryptonPanel pFemaleMonth0;
        private Krypton.Toolkit.KryptonPanel pTotalMonth2;
        private Krypton.Toolkit.KryptonPanel pTotalMonth1;
        private Krypton.Toolkit.KryptonPanel pMaleMonth1;
        private Krypton.Toolkit.KryptonPanel pFemaleMonth2;
        private Krypton.Toolkit.KryptonPanel pFemaleMonth1;
        private Krypton.Toolkit.KryptonPanel pFemaleMonth3;
        private Krypton.Toolkit.KryptonPanel kryptonPanel3;
        private Krypton.Toolkit.KryptonPanel kryptonPanel4;
        private Krypton.Toolkit.KryptonPanel kryptonPanel2;
        private Krypton.Toolkit.KryptonLabel kryptonLabel4;
        private Krypton.Toolkit.KryptonLabel kryptonLabel5;
        private Krypton.Toolkit.KryptonLabel kryptonLabel3;
        private Krypton.Toolkit.KryptonLabel kryptonLabel2;
        private Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private Panel panel2;
        private Label label15;
        private Krypton.Toolkit.KryptonPanel kryptonPanel5;
        private Krypton.Toolkit.KryptonPanel panelCharterPaid;
        private Krypton.Toolkit.KryptonPanel panelCharterPayTotal;
        private Label lbDPaid1;
        private Label label19;
        private Label lbDTotalPaid;
        private Label label21;
        private Label lbDPaid2;
        private Label label24;
        private Label lbActive;
        private Label label27;
        private Panel panelResultSum;
        private Label label18;
        private Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private Krypton.Toolkit.KryptonPanel panelCharterFemale;
        private Label lbTotalStudentFemale;
        private Krypton.Toolkit.KryptonPanel panelCharterMale;
        private Label lbTotalStudentMale;
        private Label Total;
        private Label lbTotalStudent;
        private Label label11;
        private Label lbNewStudent;
        private Label label12;
        private Label lbStopStudent;
        private Label label10;
        private Label label9;
        private Label label13;
        private Label label6;
        private Label label4;
        private Panel panelInformation;
        private Panel panelInforamtionGeneral;
        private Label label14;
        private Label lbNumberPay;
        private Panel panelInformationGtrade;
        private Krypton.Toolkit.KryptonButton btnDetailGrade;
        private Label lbNumberGrades;
        private Label label3;
        private Panel panelInforamtionTeacher;
        private Krypton.Toolkit.KryptonButton btnDetailTeacher;
        private Label lbNumberTeachers;
        private Label label2;
        private Panel panelInformationStudent;
        private Krypton.Toolkit.KryptonButton btnDetailStudent;
        private Label lbNumberStudents;
        private Label label1;
        private Krypton.Toolkit.KryptonButton btnDetailPayment;
        private Panel panel5;
        private Panel panel1;
        private Panel panel4;
        private Panel panelResultDaily;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartResultMonthly;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartFemaleMale;
        private Panel panel6;
        private Panel panel7;
        private Panel panel8;
        private Panel panel3;
        private Panel panel10;
        private Panel panel12;
        private Panel panel9;
        private Panel panel11;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartPaidIsActive;
    }
}
