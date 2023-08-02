# Sử dụng ide JetbrainRider

# Webapp : parking server
## Cài đặt và load thư viện
Trỏ terminal vào thư mục src/zero.web.mvc và sử dụng câu lệnh
```cmd
npm install -g yarn
yarn install
```
## Tạo Database
Cấu hình đường dẫn đến database trong file appsetting.json
Vào terminal, cd vào src/entityframeworkcore, chạy lệnh
```cmd
dotnet ef database update
```
## Build giao diện
cd vào thư mục src/zero.web.mvc và sử dụng câu lệnh
```cmd
gulp buildDev
```
## Chạy Project zero.web.mvc 
cd vào thư mục src/zero.web.mvc và sử dụng câu lệnh
```cmd
dotnet run
```

# Winform app : parking client
Cấu hình đường dẫn đến database, kết nối barie, kết nối đầu đọc thẻ, kết nối camera trong file app.config sau đó chạy project 

