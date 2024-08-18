create table User_Info  (

ID INT PRIMARY KEY IDENTITY (1,1),
username varchar(255),
email varchar(255),
password text,
image varchar(255)
);
CREATE TABLE Category(
ID INT PRIMARY KEY IDENTITY (1,1),
name varchar(255),
description text,
price decimal,
image varchar(max),

); 
create table Product  (

ID INT PRIMARY KEY IDENTITY (1,1),
name varchar(255),
description text,
price decimal,
image1 varchar(max),
image2 varchar(max),
image3 varchar(max),
cat_ID INT FOREIGN KEY REFERENCES Category(ID)
);

CREATE TABLE Cart(
ID INT PRIMARY KEY IDENTITY (1,1),
product_ID INT FOREIGN KEY REFERENCES Product(ID),
quantity int);


INSERT INTO Category (name, description, price, image) VALUES
('Learning Toys', 'Toys designed for early childhood development', 49.99, 'learning_toys.jpg'),
('Books', 'Montessori books for various age groups', 12.99, 'montessori_books.jpg'),
('Outdoor Play', 'Outdoor play equipment and toys for active learning', 79.99, 'outdoor_play.jpg');

INSERT INTO Product (name, description, price, image1, image2, image3, cat_ID) VALUES
('Wooden Blocks', 'Set of wooden blocks for creative building', 39.99, 'blocks1.jpg', 'blocks2.jpg', 'blocks3.jpg', 1),
('Alphabet Puzzle', 'Puzzle for learning letters', 22.99, 'alphabet_puzzle1.jpg', 'alphabet_puzzle2.jpg', 'alphabet_puzzle3.jpg', 1),
('Montessori Phonics Book', 'Book to teach early phonics', 14.99, 'phonics_book1.jpg', 'phonics_book2.jpg', 'phonics_book3.jpg', 2),
('Nature Exploration Book', 'Book to encourage exploration of nature', 16.99, 'nature_book1.jpg', 'nature_book2.jpg', 'nature_book3.jpg', 2),
('Balance Bike', 'Bike designed to develop balance in toddlers', 89.99, 'balance_bike1.jpg', 'balance_bike2.jpg', 'balance_bike3.jpg', 3),
('Sandbox Kit', 'Complete sandbox kit with toys', 129.99, 'sandbox1.jpg', 'sandbox2.jpg', 'sandbox3.jpg', 3),
('Swing Set', 'Outdoor swing set for kids', 150.00, 'swing1.jpg', 'swing2.jpg', 'swing3.jpg', 3),
('Color Sorting Toy', 'Toy to help children learn color sorting', 29.99, 'sorting_toy1.jpg', 'sorting_toy2.jpg', 'sorting_toy3.jpg', 1),
('Counting Beads', 'Beads for learning to count and do simple math', 19.99, 'counting_beads1.jpg', 'counting_beads2.jpg', 'counting_beads3.jpg', 1),
('Storybook Collection', 'Collection of stories to read aloud', 25.99, 'storybook1.jpg', 'storybook2.jpg', 'storybook3.jpg', 2);


INSERT INTO Cart (product_ID, quantity) VALUES
(1, 3),
(2, 2),
(3, 1),
(4, 4),
(5, 1),
(6, 2),
(7, 1),
(8, 3),
(9, 2),
(10, 1);

