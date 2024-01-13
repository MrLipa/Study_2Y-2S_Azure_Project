# Getting Started

Data Source=LENOVO_LIPA\SQLEXPRESS;Initial Catalog=azure-project;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False

zrboić endpoint na email i password login token zakodowany na 15 minut 
zrobić endpoint na rejestracje 
zrobić endpoint na wylogowanie
zrobić endpoint na refresh token

eventgrid topic dwa subskrupcje coś próbować 



































ogarnac sql który sie wkleja
ogarnąc autmatyczne deployment center
ogarnąc variable do robienia connection string i function stringa
ogarnąc sloty 
ogarnąć monitoring 
ogarnąc endpointy na zwrócenie limitów 
ogarnąc endpointa na obecny dzienny limi z sumy posiłków


ogarnąc funkcje (jak sie da testy i deploy na github actions)


Remove-Migration InitialCreate
Add-Migration InitialCreate
Update-Database
dotnet build
dotnet tool install -g dotnet-ef
dotnet ef migrations remove
dotnet ef migrations add NazwaMigracji
dotnet ef database update
dotnet run seeddata

dotnet ef database update --project ./Project --connection "Server=tcp:project-sql-server-2137.database.windows.net,1433;Initial Catalog=project-sql-database;Persist Security Info=False;User ID=admin2137;Password=Admin123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"


netstat -ano | findstr :5147
taskkill /F /PID [PID]

.venv\Scripts\activate
func start


da� na gitlaba
wdro�y� na azure razem z testami na githuba ctions i z baz� danych 
dodac alerty logi metryki inisgiht
doda� funkcje 
cosmosdb do logowania sesji
terraform



ssh-keygen -t rsa -b 2048 -C "tomek.szkaradek1127@gmail.com"
cat ~/.ssh/id_rsa.pub

Resource Group
App Service Plan
Webb App

Function APPP z testami 
data storage server
zrobi� zmienn� sodkowisk� kt�r� �atwo mozna zmienicac connection string
terraform


terraform --version
az version cli przynajmniej 2
az upgrade
az login tutaj mam crudentiale
.azure tam pliki acccess token itd
az config set core.allow_broker=true
az account clear
az login

terraform init
terraform plan # This will output the changes on the Azure env that terraform will make
terrafrom apply -auto-approve

terraform init
terraform fmt
terraform validate
terraform plan
terraform apply
yes
terraform destroy


pip install virtualenv
python -m venv env
env\Scripts\activate
pip install -r requirements.txt
deactivate

2 web app
sql
blob
wdra�anie testy na azurze
azure function
cosmosdb
alerty logowanie metryki aplication ingist
terraform



micorserwis 
rejestracja
logowanie
pobranie(wszystkie/konkretny) dodawanie usuwanie edycja usera
dodanie usuniecie posi�ku UserMeals zjedzonego

mikroserwis
pobranie(wszystkie/konkretny) dodawanie usuwanie edycja produkt�w

mikroserwis
pobranie(wszystkie/konkretny) dodawanie usuwanie edycja posi�k�w dodanie tranzakcji
Odpalenie Algorytmu Podajesz List(IDS) Limity Tworzy Posi�ek zapisuje do bazy danych




user dodaje do siebie posi�k�w ile chce 
wystielenie ile zjad� 
ustawnie limit�w
przy ka�dym posi�ku liczy i ostrzega
mo�e dodac sw�j albo poprosi alborytm o spersjonalizowanie
sworzenie posi�ku na podstaiw listy produkt�w 
odpalanie funckji po podaniu limity i odpalenie funkcji
zapisanie wyniku do hsitori 
wy�wietlenie histori

