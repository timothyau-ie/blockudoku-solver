
namespace BSolver.UI
{
    partial class Form1
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnAI = new System.Windows.Forms.Button();
            this.btnClearSelection = new System.Windows.Forms.Button();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.btnRandomSelection = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAI
            // 
            this.btnAI.Location = new System.Drawing.Point(953, 654);
            this.btnAI.Name = "btnAI";
            this.btnAI.Size = new System.Drawing.Size(57, 54);
            this.btnAI.TabIndex = 0;
            this.btnAI.Text = "AI";
            this.btnAI.UseVisualStyleBackColor = true;
            this.btnAI.Click += new System.EventHandler(this.btnAI_Click);
            // 
            // btnClearSelection
            // 
            this.btnClearSelection.Location = new System.Drawing.Point(1016, 654);
            this.btnClearSelection.Name = "btnClearSelection";
            this.btnClearSelection.Size = new System.Drawing.Size(71, 54);
            this.btnClearSelection.TabIndex = 0;
            this.btnClearSelection.Text = "clear selection";
            this.btnClearSelection.UseVisualStyleBackColor = true;
            this.btnClearSelection.Click += new System.EventHandler(this.btnClearSelection_Click);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(953, 714);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(57, 22);
            this.numericUpDown1.TabIndex = 1;
            // 
            // btnRandomSelection
            // 
            this.btnRandomSelection.Location = new System.Drawing.Point(1093, 654);
            this.btnRandomSelection.Name = "btnRandomSelection";
            this.btnRandomSelection.Size = new System.Drawing.Size(76, 54);
            this.btnRandomSelection.TabIndex = 0;
            this.btnRandomSelection.Text = "Random Selection";
            this.btnRandomSelection.UseVisualStyleBackColor = true;
            this.btnRandomSelection.Click += new System.EventHandler(this.btnRandomSelection_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1715, 846);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.btnClearSelection);
            this.Controls.Add(this.btnRandomSelection);
            this.Controls.Add(this.btnAI);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnAI;
        private System.Windows.Forms.Button btnClearSelection;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Button btnRandomSelection;
    }
}

