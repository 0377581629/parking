# Webapp : Parking_server

## Setting and load library

cd to src/zero.web.mvc and run 

```cmd
npm install -g yarn
yarn install
```

## Create Database

Config connection string to your database in file appsetting.json
cd to src/entityframeworkcore and run

```cmd
dotnet ef database update
```

## Build UI

cd to src/zero.web.mvc and run

```cmd
gulp buildDev
```

## Run Project zero.web.mvc to open web app

cd to src/zero.web.mvc and run

```cmd
dotnet run
```

# Winform app : Parking_client

Config connection string to database, connect barie, config card reader, config camera in file app.config and run project after that

# License plate detection and recognition: License-plate-flask

## Setting virtualenv

Run this cmd in your computer

```cmd
pip install virtualenv
```

## create virtual environment for project

cd to project folder and run

```cmd
python -m venv venv
cd venv/Script
Activate.ps1
```

## Install essential library to run project

cd to the folder contain project and run

```cmd
pip install -r requirements.txt
```

## Run Project

```cmd
py app.py
```
