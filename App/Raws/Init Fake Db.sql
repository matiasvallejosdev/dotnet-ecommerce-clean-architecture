INSERT INTO Brands (Name, CreatedAt, UpdatedAt, IsDown, DownAt)
VALUES
('BrandA', GETDATE(), GETDATE(), 0, NULL),
('BrandB', GETDATE(), GETDATE(), 0, NULL),
('BrandC', GETDATE(), GETDATE(), 0, NULL),
('BrandD', GETDATE(), GETDATE(), 1, GETDATE());

INSERT INTO Families (Name, CreatedAt, UpdatedAt, IsDown, DownAt)
VALUES
('FamilyX', GETDATE(), GETDATE(), 0, NULL),
('FamilyY', GETDATE(), GETDATE(), 0, NULL),
('FamilyZ', GETDATE(), GETDATE(), 0, NULL),
('FamilyW', GETDATE(), GETDATE(), 1, GETDATE());
