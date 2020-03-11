
-- SELECT * FROM Users;
-- SELECT * FROM Vendors;
-- SELECT * FROM Products;
-- SELECT * FROM Requests;

SELECT rl.Id,
       rl.ProductId,
       rl.Quantity,
       rl.RequestId
FROM RequestLines rl
  JOIN Requests r
    ON rl.RequestId = r.Id
  WHERE r.Status = 'APPROVED'; 

SELECT r.Id AS 'Request ID', 
       r.Description,
       r.UserId,
       r.Status,
       r.Total,
       rl.Id AS 'ReqLine ID',
       p.Id AS 'Product ID',
       p.Name AS 'Product',
       p.Price AS 'Unit Price',
       rl.Quantity AS 'RL Quantity',
       v.Id AS 'Vendor ID',
       v.Name AS 'Vendor'
  FROM Requests r
  FULL JOIN RequestLines rl 
    ON r.Id = rl.RequestId
  JOIN Products p
    ON rl.ProductId = p.Id
  JOIN Vendors v
    ON p.VendorId = v.Id
  WHERE r.Status = 'APPROVED'
  ORDER BY p.Id;

SELECT v.Id AS 'Vendor ID',
       p.Id AS 'Product ID',
       p.Name AS 'Product',
       p.Price AS 'Unit Price',
       SUM(p.Price * rl.Quantity) AS 'POLine Price',
       SUM(rl.Quantity) AS 'POL Quantity'
  FROM Requests r
  FULL JOIN RequestLines rl 
    ON r.Id = rl.RequestId
  JOIN Products p
    ON rl.ProductId = p.Id
  JOIN Vendors v
    ON p.VendorId = v.Id
  WHERE r.Status = 'APPROVED'
  GROUP BY p.Id, p.Name, v.Id, p.Price;