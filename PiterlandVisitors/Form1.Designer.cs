namespace PiterlandVisitors
{
    partial class Form1
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.visitorsTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.SuspendLayout();
            // 
            // visitorsTableLayoutPanel
            // 
            this.visitorsTableLayoutPanel.AutoScroll = true;
            this.visitorsTableLayoutPanel.ColumnCount = 7;
            this.visitorsTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.visitorsTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.visitorsTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.visitorsTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.visitorsTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.visitorsTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.visitorsTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.visitorsTableLayoutPanel.Location = new System.Drawing.Point(13, 13);
            this.visitorsTableLayoutPanel.Name = "visitorsTableLayoutPanel";
            this.visitorsTableLayoutPanel.RowCount = 1;
            this.visitorsTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.visitorsTableLayoutPanel.Size = new System.Drawing.Size(744, 304);
            this.visitorsTableLayoutPanel.TabIndex = 7;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(764, 322);
            this.Controls.Add(this.visitorsTableLayoutPanel);
            this.Name = "Form1";
            this.Text = "Таймер";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel visitorsTableLayoutPanel;
    }
}

