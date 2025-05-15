#Tinkr API 

## Starting Sql Server

```powershell
$sa_password = "[SA Password HERE]
 docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=$sa_password" -e "MSSQL_PID=Evaluation" -p 1433:1433 -v sqltinkrvolume:/var/opt/mssql -d --rm --name tinkrsqlpreview --hostname tinkrsqlpreview mcr.microsoft.com/mssql/server:2022-preview-ubuntu-22.04
```

## Set connection string to secret manager

```powershell
$sa_password = "[SA Password HERE]
dotnet user-secrets set "ConnectionStrings:TinkrProjectsContext" "Server=localhost;Database=TinkrProjects;User Id=sa;Password=$sa_password;TrustServerCertificate=True;"
```