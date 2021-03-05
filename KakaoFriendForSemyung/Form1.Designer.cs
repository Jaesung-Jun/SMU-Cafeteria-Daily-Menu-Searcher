namespace KakaoFriendForSemyung
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.InsertIntoDB = new System.Windows.Forms.Button();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.monthCalendar1 = new System.Windows.Forms.MonthCalendar();
            this.Database_list = new System.Windows.Forms.ListView();
            this.Load_DB = new System.Windows.Forms.Button();
            this.CafeID = new System.Windows.Forms.TextBox();
            this.Search_text = new System.Windows.Forms.TextBox();
            this.Search_Button = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.Developer_Mode = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // InsertIntoDB
            // 
            this.InsertIntoDB.Location = new System.Drawing.Point(12, 230);
            this.InsertIntoDB.Name = "InsertIntoDB";
            this.InsertIntoDB.Size = new System.Drawing.Size(220, 29);
            this.InsertIntoDB.TabIndex = 0;
            this.InsertIntoDB.Text = "학식정보 DB에 넣기";
            this.InsertIntoDB.UseVisualStyleBackColor = true;
            this.InsertIntoDB.Click += new System.EventHandler(this.InsertIntoDB_Click);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(12, 192);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(220, 21);
            this.dateTimePicker1.TabIndex = 1;
            // 
            // monthCalendar1
            // 
            this.monthCalendar1.Location = new System.Drawing.Point(12, 18);
            this.monthCalendar1.Name = "monthCalendar1";
            this.monthCalendar1.TabIndex = 2;
            // 
            // Database_list
            // 
            this.Database_list.Location = new System.Drawing.Point(244, 18);
            this.Database_list.Name = "Database_list";
            this.Database_list.Size = new System.Drawing.Size(971, 353);
            this.Database_list.TabIndex = 3;
            this.Database_list.UseCompatibleStateImageBehavior = false;
            // 
            // Load_DB
            // 
            this.Load_DB.Location = new System.Drawing.Point(12, 265);
            this.Load_DB.Name = "Load_DB";
            this.Load_DB.Size = new System.Drawing.Size(220, 29);
            this.Load_DB.TabIndex = 4;
            this.Load_DB.Text = "DB 정보 불러오기";
            this.Load_DB.UseVisualStyleBackColor = true;
            this.Load_DB.Click += new System.EventHandler(this.Load_DB_Click);
            // 
            // CafeID
            // 
            this.CafeID.Location = new System.Drawing.Point(116, 300);
            this.CafeID.Name = "CafeID";
            this.CafeID.Size = new System.Drawing.Size(116, 21);
            this.CafeID.TabIndex = 5;
            // 
            // Search_text
            // 
            this.Search_text.Location = new System.Drawing.Point(116, 327);
            this.Search_text.Name = "Search_text";
            this.Search_text.Size = new System.Drawing.Size(116, 21);
            this.Search_text.TabIndex = 6;
            // 
            // Search_Button
            // 
            this.Search_Button.Location = new System.Drawing.Point(12, 327);
            this.Search_Button.Name = "Search_Button";
            this.Search_Button.Size = new System.Drawing.Size(98, 23);
            this.Search_Button.TabIndex = 8;
            this.Search_Button.Text = "검색";
            this.Search_Button.UseVisualStyleBackColor = true;
            this.Search_Button.Click += new System.EventHandler(this.Search_Button_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(12, 300);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(98, 23);
            this.button3.TabIndex = 10;
            this.button3.Text = "식당정보";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // Developer_Mode
            // 
            this.Developer_Mode.Location = new System.Drawing.Point(1096, 377);
            this.Developer_Mode.Name = "Developer_Mode";
            this.Developer_Mode.Size = new System.Drawing.Size(119, 23);
            this.Developer_Mode.TabIndex = 11;
            this.Developer_Mode.Text = "개발자모드";
            this.Developer_Mode.UseVisualStyleBackColor = true;
            this.Developer_Mode.Click += new System.EventHandler(this.Developer_Mode_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1227, 405);
            this.Controls.Add(this.Developer_Mode);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.Search_Button);
            this.Controls.Add(this.Search_text);
            this.Controls.Add(this.CafeID);
            this.Controls.Add(this.Load_DB);
            this.Controls.Add(this.Database_list);
            this.Controls.Add(this.monthCalendar1);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.InsertIntoDB);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button InsertIntoDB;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.MonthCalendar monthCalendar1;
        private System.Windows.Forms.ListView Database_list;
        private System.Windows.Forms.Button Load_DB;
        private System.Windows.Forms.TextBox CafeID;
        private System.Windows.Forms.TextBox Search_text;
        private System.Windows.Forms.Button Search_Button;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button Developer_Mode;
    }
}

