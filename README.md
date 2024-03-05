# Webapp : Parking_server

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

# Winform app : Parking_client

Cấu hình đường dẫn đến database, kết nối barie, kết nối đầu đọc thẻ, kết nối camera trong file app.config sau đó chạy project

# Nhận diện biển số xe: License-plate-flask

## Cài đặt virtualenv

Vào terminal mặc định trên máy tính, chạy lệnh

```cmd
pip install virtualenv
```

## Tạo môi trường ảo cho project

Vào terminal, cd vào project folder, chạy lệnh

```cmd
python -m venv venv
cd venv/Script
Activate.ps1
```

## Install các thư viện cần thiết để chạy project

cd vào thư mục chứa project và chạy câu lệnh trong terminal

```cmd
pip install -r requirements.txt
```

## Chạy Project

```cmd
py app.py
```
