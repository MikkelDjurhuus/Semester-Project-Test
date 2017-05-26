mongoimport -d Guttenberg -c City --type csv --file .\cities15000.csv --headerline
mongo localhost:27017/Guttenberg .\City_Location.js
mongoimport -d Guttenberg -c Book --type csv --file .\book.csv --headerline
mongo localhost:27017/Guttenberg .\Book_Cities.js
#mongoimport -d Guttenberg -c refs --type csv --file .\mentions.csv --headerline