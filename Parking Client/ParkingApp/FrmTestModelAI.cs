using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Windows.Forms;

namespace ParkingApp
{
    public partial class FrmTestModelAI : MetroFramework.Forms.MetroForm
    {
        public FrmTestModelAI()
        {
            InitializeComponent();
        }

        private void btnChonFile_Click(object sender, EventArgs e)
        {
            // Hiển thị dialog chọn tệp
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png, *.bmp) | *.jpg; *.jpeg; *.png; *.bmp";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Lấy đường dẫn tệp được chọn
                    string filePath = openFileDialog.FileName;

                    // Hiển thị đường dẫn tệp trong textbox
                    textBox1.Text = filePath;
                }
            }
        }

        private async void btnUploadFile_Click(object sender, EventArgs e)
        {
            string url = "http://localhost:5000/upload"; // URL của API Python
            string filePath = textBox1.Text; // Đường dẫn đến tệp ảnh cần gửi lên API

            // Kiểm tra xem người dùng đã chọn tệp hay chưa
            if (string.IsNullOrEmpty(filePath))
            {
                MessageBox.Show("Vui lòng chọn tệp ảnh cần gửi lên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var client = new HttpClient())
            {
                using (var content = new MultipartFormDataContent())
                {
                    // Đọc dữ liệu từ tệp ảnh và thêm vào nội dung yêu cầu POST
                    byte[] imageBytes = File.ReadAllBytes(filePath);
                    var imageContent = new ByteArrayContent(imageBytes);
                    content.Add(imageContent, "image", "image.jpg");

                    // Gửi yêu cầu POST đến địa chỉ API
                    var response = await client.PostAsync(url, content);

                    // Đọc phản hồi từ máy chủ
                    string responseString = await response.Content.ReadAsStringAsync();
                    MessageBox.Show(responseString);
                }
            }
        }
    }
}