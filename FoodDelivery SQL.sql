CREATE DATABASE FoodDelivery

USE FoodDelivery

--tao login vao SQLServer
CREATE LOGIN userFoodDelivery
WITH PASSWORD = '12345';
--tao user vao database
CREATE USER userFoodDelivery
FOR LOGIN userFoodDelivery;
--phan quyen user thanh owner
Exec sp_addrolemember 'db_owner', 'userFoodDelivery';

--table nguoidung
CREATE TABLE nguoidung (
	MaNguoiDung int IDENTITY(1,1) PRIMARY KEY,
	TenNguoiDung nvarchar(500),
	TenDangNhap nvarchar(100),
	MatKhau nvarchar(100),
	Email nvarchar(100),
	SoDienThoai varchar(50),
	DiaChi nvarchar(500)
)

--table danh muc mon an
CREATE table ProductCategory (
	CategoryID int NOT NULL IDENTITY(1,1) PRIMARY KEY,
	CategoryName nvarchar(250),
	CategoryImage nvarchar(250)
)
--table cua hang
CREATE table Seller (
	SellerID int NOT NULL IDENTITY(1,1) PRIMARY KEY,
	SellerName nvarchar(250),
	SellerImage nvarchar(250),
	SellerHeroBanner nvarchar(250),
	SellerAddress nvarchar(500),
	SellerRating numeric(2,1),
	isFeatured BIT DEFAULT 'false' NOT NULL,
)
--table mon an
CREATE table Product (
	ProductID int NOT NULL IDENTITY(1,1) PRIMARY KEY,
	ProductName nvarchar(250),
	CategoryID int FOREIGN KEY REFERENCES nguoidung(CategoryID),
	SellerID int FOREIGN KEY REFERENCES Seller(SellerID),
	ProductPrice int,
	ProductImage nvarchar(250)
)
--table don hang
CREATE table SalesOrder (
	OrderID int NOT NULL IDENTITY(1,1) PRIMARY KEY,
	CustomerID int FOREIGN KEY REFERENCES nguoidung(MaNguoiDung),
	SellerID int FOREIGN KEY REFERENCES Seller(SellerID),
	OrderDate datetime,
	OrderStatus nvarchar(250),
	ShippingAddress nvarchar(500),
	PaymentMethod nvarchar(250),
	TotalAmount int
)
--table chi tiet don hang
CREATE table SalesOrderItem (
	ItemID int NOT NULL IDENTITY(1,1) PRIMARY KEY,
	OrderID int FOREIGN KEY REFERENCES SalesOrder(OrderID),
	ProductID int FOREIGN KEY REFERENCES Product(ProductID),
	ProductName nvarchar(500),
	Qty int,
	ProductPrice int,
	TotalItemAmount int
)

--insert data vao table nguoidung
INSERT INTO nguoidung (TenNguoiDung, TenDangNhap, MatKhau, Email, SoDienThoai)
VALUES ('phu tran','ptran1','12345','ptran1@gmail.com', '0945600864')

--insert data vao table seller
INSERT INTO Seller(SellerName, SellerImage, SellerHeroBanner, SellerAddress, SellerRating, isFeatured)
VALUES (N'Cơm tấm Phúc Lộc Thọ', 'com_tam_phuc_loc_tho.jpeg', 'com_tam_phuc_loc_tho_banner.jpg', N'Bàu Cát', 4.7, 'true'),
	   (N'Phờ Hòa', 'pho_hoa.jpg', 'pho_hoa_banner.jpg', N'260 Pasteur', 4.5, 'true'),
	   (N'Highland Coffee', 'highland.jpg', 'highland_banner.jpg', N'CMT8', 4.6, 'true'),
	   (N'Phờ Bình', 'pho_binh.jpg', 'pho_binh_banner.jpg', N'Nguyễn Trãi', 4.2, 'false'),
	   (N'Bún bò Huế Xưa', 'hue_xua.jpg', 'hue_xua_banner.jpg', N'45 Bàu Cát', 4.1, 'false'),
	   (N'KFC', 'kfc.jpeg', 'kfc_banner.jpg', N'16C Lê Duẩn', 4.8, 'true'),
	   (N'Koi Thé', 'koi_the.jpeg', 'koi_the_banner.jpg', N'47A CMT8', 4.9, 'true'),
	   (N'Mc Donald', 'mc_donald.jpg', 'mc_donald_banner.jpg', N'187 Cộng hòa', 4.3, 'false'),
	   (N'Mì chú Xá', 'mi_chu_xa.jpeg', 'mi_chu_xa_banner.jpg', N'92 Lê Lợi', 3.9, 'false'),
	   (N'Pizza Hut', 'pizzahut.jpg', 'pizzahut_banner.jpg', N'7A Nguyễn Du', 4.2, 'true')

--insert data vao table ProductCategory
insert into ProductCategory(CategoryName,CategoryImage)
values (N'Cơm','cate_1.png'),
		 (N'Phở','cate_2.png'),
		 (N'Gà quay','cate_3.png'),
		 (N'Pizza','cate_4.png'),
		 (N'Nước uống','cate_5.png'),
		 (N'Đồ ăn nhanh','cate_6.png'),
		 (N'Mì','cate_8.png');

--insert data vao table Product
insert into Product(ProductName, CategoryID, SellerID, ProductPrice, ProductImage)
values (N'Phở tái', 8, 2, 45000, 'pho_tai.jpg'),
	   (N'Phở tái nạm', 8, 2, 50000, 'pho_tai_nam.jpg'),
	   (N'Phở tái viên', 8, 2, 50000, 'pho_tai_vien.jpg'),
	   (N'Phở thập cẩm', 8, 2, 60000, 'pho_thap_cam.jpeg'),
       (N'Phở cuốn', 8, 2, 40000, 'pho_cuon.jpg'),
	   (N'Phở tái', 8, 4, 45000, 'pho_tai.jpg'),
	   (N'Phở tái nạm', 8, 4, 50000, 'pho_tai_nam.jpg'),
	   (N'Phở tái viên', 8, 4, 50000, 'pho_tai_vien.jpg'),
	   (N'Phở thập cẩm', 8, 4, 60000, 'pho_thap_cam.jpeg'),
       (N'Phở cuốn', 8, 4, 40000, 'pho_cuon.jpg'),
	   (N'Cơm sườn', 7, 1, 35000, 'com_suon.jpg'),
		(N'Cơm sườn bì chả', 7, 1, 55000, 'com_suon_bi_cha.jpg'),
		(N'Cơm gà quay', 7, 1, 40000, 'com_ga_quay.jpg'),
		(N'Cơm bò', 7, 1, 45000, 'com_bo.jpg'),
		(N'Mì hoành thánh', 13, 9, 45000, 'mi_hoanh_thanh.jpg'),
		(N'Mì thịt', 13, 9, 30000, 'mi_thit.png'),
		(N'Mì xá xíu', 13, 9, 40000, 'mi_xa_xiu.jpg'),
		(N'Mì xương', 13, 9, 45000, 'mi_xuong.jpg'),
		(N'Bún bò', 13, 5, 45000, 'bun_bo.jpg'),
		(N'Bún bò tái', 13, 5, 40000, 'bun_bo_tai.jpg'),
		(N'Bún bò thập cẩm', 13, 5, 50000, 'bun_bo_thap_cam.jpg'),
		(N'Gà chiên', 9, 6, 80000, 'ga_chien.jpg'),
		(N'Gà quay', 9, 6, 95000, 'ga_quay.jpg'),
		(N'Pizza thịt', 10, 10, 120000, 'pizza_thit.jpg'),
		(N'Pizza hải sản', 10, 10, 150000, 'pizza_hai_san.jpg'),
		(N'Cafe đen', 11, 3, 30000, 'cafe_den.jpg'),
		(N'Cafe mocha', 11, 3, 35000, 'cafe_mocha.jpg'),
		(N'Trà đào', 11, 3, 40000, 'tra_dao.jpg'),
		(N'Trà sen vàng', 11, 3, 40000, 'tra_sen_vang.jpeg'),
		(N'Cafe đen', 11, 7, 30000, 'cafe_den.jpg'),
		(N'Cafe mocha', 11, 7, 35000, 'cafe_mocha.jpg'),
		(N'Trà đào', 11, 7, 40000, 'tra_dao.jpg'),
		(N'Trà sen vàng', 11, 7, 40000, 'tra_sen_vang.jpeg'),
		(N'Combo hamburger', 12, 8, 85000, 'hamburger_combo.png'),
		(N'Hamburger', 12, 8, 70000, 'hamburger.jpg')		

--procedure lay user
CREATE proc getUser (
@tendangnhap nvarchar(100),
@matkhau nvarchar(100)
)
as
select [MaNguoiDung]
      ,[TenNguoiDung]
      ,[TenDangNhap]
      ,[MatKhau]
      ,[Email]
	  ,[SoDienThoai]
	  ,[DiaChi]
from NguoiDung
where [TenDangNhap] = @tendangnhap
		AND [MatKhau] = @matkhau;

--procedure update user
CREATE proc updateUser (
@MaNguoiDung int,
@TenNguoiDung nvarchar(500),
@TenDangNhap nvarchar(100),
@MatKhau nvarchar(100),
@Email nvarchar(100),
@SoDienThoai varchar(50),
@DiaChi nvarchar(500),
@CurrentID int output
)
as
begin
	if (not exists (select * from nguoidung where TenDangNhap = @TenDangNhap))
		begin
			set @CurrentID = -1;
			return
		end
	update nguoidung
	set TenNguoiDung = @TenNguoiDung
		,MatKhau = @MatKhau
		,Email = @Email
		,SoDienThoai = @SoDienThoai
		,DiaChi = @DiaChi
	where MaNguoiDung = @MaNguoiDung;
	set @CurrentID = @MaNguoiDung;
end

--procedure tao user moi
CREATE proc addUser (
@TenNguoiDung nvarchar(500),
@TenDangNhap nvarchar(100),
@MatKhau nvarchar(100),
@Email nvarchar(100),
@SoDienThoai varchar(50),
@DiaChi nvarchar(500),
@CurrentID int output
)
as
begin
	if (exists (select * from nguoidung where TenDangNhap = @TenDangNhap))
		begin
			set @CurrentID = -1;
			return
		end
	insert into nguoidung(TenNguoiDung,TenDangNhap,MatKhau,Email,SoDienThoai,DiaChi)
	values (@TenNguoiDung, @TenDangNhap, @MatKhau, @Email, @SoDienThoai,@DiaChi)
	set @CurrentID = @@IDENTITY
end

--procedure lay Product Category
CREATE proc GetProductCategory
AS
begin
	select * from ProductCategory;
end

--procedure lay Seller Featured List
CREATE proc GetSellerFeaturedList
AS
begin
	select * from Seller where isFeatured = 1;
end

--procedure lay All Seller
CREATE proc GetAllSeller
AS
begin
	select * from Seller;
end

--procedure lay danh sach Seller theo category
CREATE proc GetSellerByCategory (
@CategoryID int
)
as
begin
	with sl as (
		select distinct SellerID
		from Product
		where CategoryID = @CategoryID
		)
	select *
	from Seller
	where SellerID IN (select SellerID from sl);
end

--procedure lay mot seller
CREATE proc GetSellerDetail (
@SellerID int
)
AS
begin
	select * from Seller where SellerID = @SellerID;
end

--procedure lay Product theo Seller
CREATE proc GetProductBySeller (
@SellerID int
)
AS
begin
	select * from Product where SellerID = @SellerID;
end

--procedure tao don hang
CREATE proc createSalesOrder (
@CustomerID int,
@SellerID int,
@OrderDate datetime,
@OrderStatus nvarchar(250),
@ShippingAddress nvarchar(500),
@PaymentMethod nvarchar(250),
@TotalAmount int,
@CurrentID int output
)
as
begin
	insert into SalesOrder(CustomerID, SellerID, OrderDate, OrderStatus, ShippingAddress, PaymentMethod, TotalAmount)
	values (@CustomerID, @SellerID, @OrderDate, @OrderStatus, @ShippingAddress, @PaymentMethod, @TotalAmount);
	set @CurrentID = @@IDENTITY
end

--procedure tao chi tiet don hang
CREATE proc createSalesOrderItem (
@OrderID int,
@ProductID int,
@ProductName nvarchar(500),
@Qty int,
@ProductPrice int,
@TotalItemAmount int,
@CurrentID int output
)
as
begin
	insert into SalesOrderItem (OrderID, ProductID, ProductName, Qty, ProductPrice, TotalItemAmount)
	values (@OrderID, @ProductID, @ProductName, @Qty, @ProductPrice, @TotalItemAmount);
	set @CurrentID = @@IDENTITY
end

--procedure lay danh sach don hang
CREATE proc GetOrderList (
@CustomerID int
)
as
begin
	select  so.OrderID,
			so.CustomerID,
			so.SellerID,
			CONCAT(s.SellerName,' - ',s.SellerAddress) AS SellerNameAddress,
			so.OrderDate,
			so.OrderStatus,
			so.ShippingAddress,
			so.PaymentMethod,
			so.TotalAmount
	from SalesOrder so
		inner join Seller s ON s.SellerID = so.SellerID
	where CustomerID = @CustomerID
	order by OrderDate DESC
end

--procedure lay chi tiet mot don hang
CREATE proc GetOrderItem (
@OrderID int
)
as
begin
	select * from SalesOrderItem
	where OrderID = @OrderID
end

