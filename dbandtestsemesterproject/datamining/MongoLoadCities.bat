mongoimport -d Guttenberg -c City --type csv --file .\cities15000.csv --headerline
mongo localhost:27017/Guttenberg .\City_Location.js
