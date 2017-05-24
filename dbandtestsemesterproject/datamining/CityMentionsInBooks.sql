SELECT b.author, b.title, c.name, c.lat, c.lon, count(*) as mentioned FROM gutenberg.mentions m
join gutenberg.book b on b.id = m.book_id
join gutenberg.city c on c.id = m.city_id
group by c.name
order by b.title