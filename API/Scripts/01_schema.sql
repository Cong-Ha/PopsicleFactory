CREATE SCHEMA IF NOT EXISTS popsicle_factory;
USE popsicle_factory;
    
CREATE TABLE IF NOT EXISTS Popsicles (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    `Name` VARCHAR(50) NOT NULL,
    `Type` INT,
    `Size` INT,
    Description VARCHAR(500),
    is_sugar_free BOOLEAN DEFAULT FALSE,
    is_organic BOOLEAN DEFAULT FALSE,
    Price DECIMAL(10, 2) NOT NULL
);