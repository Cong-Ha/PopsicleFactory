USE popsicle_factory;
    
-- Clear existing data
-- DELETE FROM Popsicles;

INSERT INTO Popsicles (`Name`, `Type`, `Size`, Description, is_sugar_free, is_organic, Price) VALUES
-- Fruit Popsicles
('Summer Strawberry', 0, 1, 'Fresh strawberry popsicle made with real fruit juice', FALSE, TRUE, 2.99),
('Tropical Mango', 0, 2, 'Sweet and tangy mango popsicle with chunks of real fruit', FALSE, TRUE, 3.49),
('Wild Berry Blast', 0, 1, 'Mixed berry popsicle with strawberries, blueberries, and raspberries', FALSE, FALSE, 2.79),
('Citrus Zing', 0, 0, 'Refreshing orange and lemon popsicle perfect for kids', FALSE, FALSE, 1.99),
('Watermelon Wonder', 0, 3, 'Cool and refreshing watermelon popsicle with seeds', FALSE, TRUE, 4.29),
('Pineapple Paradise', 0, 1, 'Tropical pineapple popsicle with coconut flakes', FALSE, FALSE, 3.19),

-- Cream Popsicles
('Vanilla Dream', 1, 1, 'Classic vanilla cream popsicle with a smooth texture', FALSE, FALSE, 3.99),
('Chocolate Fudge', 1, 2, 'Rich chocolate cream popsicle with fudge swirls', FALSE, FALSE, 4.49),
('Cookies & Cream', 1, 3, 'Vanilla cream popsicle loaded with chocolate cookie pieces', FALSE, FALSE, 5.29),
('Strawberry Cream', 1, 1, 'Creamy strawberry popsicle with real fruit pieces', FALSE, FALSE, 3.79),
('Caramel Swirl', 1, 2, 'Vanilla cream popsicle with ribbons of caramel', FALSE, FALSE, 4.79),

-- Juice Popsicles
('Apple Juice Pop', 2, 0, '100% apple juice popsicle, perfect for toddlers', TRUE, TRUE, 2.29),
('Grape Juice Delight', 2, 1, 'Pure grape juice popsicle with no added sugar', TRUE, FALSE, 2.59),
('Cranberry Splash', 2, 1, 'Tart cranberry juice popsicle with natural sweeteners', TRUE, TRUE, 2.89),
('Orange Juice Classic', 2, 0, 'Fresh orange juice popsicle packed with vitamin C', TRUE, FALSE, 2.19),

-- Yogurt Popsicles
('Greek Honey Yogurt', 3, 1, 'Creamy Greek yogurt popsicle with natural honey', FALSE, TRUE, 3.29),
('Berry Yogurt Swirl', 3, 2, 'Vanilla yogurt popsicle with mixed berry swirls', FALSE, FALSE, 3.89),
('Peach Yogurt Bliss', 3, 1, 'Smooth peach yogurt popsicle with real peach pieces', FALSE, TRUE, 3.49),
('Coconut Yogurt Tropical', 3, 2, 'Coconut yogurt popsicle with tropical fruit flavors', FALSE, TRUE, 4.19),

-- Sherbet Popsicles
('Rainbow Sherbet', 4, 3, 'Tri-color sherbet popsicle with orange, lime, and raspberry', FALSE, FALSE, 4.99),
('Lemon Lime Sherbet', 4, 1, 'Refreshing citrus sherbet popsicle with a tangy kick', FALSE, FALSE, 3.39),
('Orange Sherbet Supreme', 4, 2, 'Creamy orange sherbet popsicle with real orange zest', FALSE, FALSE, 3.99),

-- Ice Cream Popsicles
('Neapolitan Bar', 5, 3, 'Classic three-flavor ice cream bar with chocolate, vanilla, and strawberry', FALSE, FALSE, 5.99),
('Mint Chocolate Chip', 5, 2, 'Cool mint ice cream popsicle loaded with chocolate chips', FALSE, FALSE, 4.89),
('Rocky Road Adventure', 5, 3, 'Chocolate ice cream popsicle with marshmallows and nuts', FALSE, FALSE, 6.29),
('Butter Pecan Delight', 5, 2, 'Rich butter pecan ice cream popsicle with candied pecans', FALSE, FALSE, 5.49),
('Birthday Cake Blast', 5, 1, 'Vanilla ice cream popsicle with colorful sprinkles and cake pieces', FALSE, FALSE, 4.59),

-- Sugar-Free Options
('Sugar-Free Cherry', 0, 1, 'Sweet cherry popsicle made with natural stevia sweetener', TRUE, FALSE, 3.29),
('Sugar-Free Chocolate', 1, 1, 'Rich chocolate cream popsicle sweetened with monk fruit', TRUE, FALSE, 4.29),
('Sugar-Free Vanilla', 5, 2, 'Classic vanilla ice cream popsicle with zero added sugar', TRUE, FALSE, 4.99),

-- Organic Options
('Organic Blueberry', 0, 1, 'Premium organic blueberry popsicle with antioxidants', FALSE, TRUE, 4.49),
('Organic Coconut Cream', 1, 2, 'Luxurious organic coconut cream popsicle', FALSE, TRUE, 5.29),
('Organic Green Apple', 2, 1, 'Crisp organic green apple juice popsicle', TRUE, TRUE, 3.79);
