ssh -i id_rsa dfgs@10.0.10.12 "sudo /usr/bin/systemctl stop Automasipp.service"
scp -i id_rsa bin\Debug\net8.0\appsettings.json dfgs@10.0.10.12:/var/www/Automasipp 
scp -i id_rsa bin\Debug\net8.0\Automasipp.Backend.runtimeconfig.json dfgs@10.0.10.12:/var/www/Automasipp 
scp -i id_rsa bin\Debug\net8.0\*.dll dfgs@10.0.10.12:/var/www/Automasipp 
ssh -i id_rsa dfgs@10.0.10.12 "sudo /usr/bin/systemctl start Automasipp.service"
ssh -i id_rsa dfgs@10.0.10.12 "sudo /usr/bin/systemctl status Automasipp.service"