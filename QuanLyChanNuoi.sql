
Create Table Tinh(
	MaTinh varchar(20) primary key,
	TenTinh nvarchar(50)
)
GO

Create Table Huyen(
	MaHuyen varchar(20) primary key,
	TenHuyen nvarchar(50),
	MaTinh varchar(20)
	FOREIGN KEY (MaTinh) REFERENCES Tinh(MaTinh)
)
GO

Create Table Xa(
	MaXa varchar(20) primary key,
	TenXa nvarchar(50),
	MaHuyen varchar(20)
	FOREIGN KEY (MaHuyen) REFERENCES Huyen(MaHuyen)
)
GO

Create Table Nguoi_dung(
	MaND varchar(20) primary key,
	TenDN varchar(20),
	MatKhau varchar(20),
	TenHienThi nvarchar(50),
	MaXa varchar(20)
	FOREIGN KEY (MaXa) REFERENCES Xa(MaXa)
)
GO

Create Table LoaiCoSo (
	MaLCS varchar(20) primary key,
	TenLCS nvarchar(50)
)
GO

Create Table CoSoSanXuatSP(
	MaSX varchar(20) primary key,
	TenSX nvarchar(50),
	SoDT char(10),
	MaXa varchar(20),
	MaLCS varchar(20)
	FOREIGN KEY (MaXa) REFERENCES Xa(MaXa),
	FOREIGN KEY (MaLCS) REFERENCES LoaiCoSo(MaLCS) 
)
GO

Create Table CoSoKhaoNghiemSP(
	MaKN varchar(20) primary key,
	TenKN nvarchar(50),
	SoDT char(10),
	MaXa varchar(20),
	MaLCS varchar(20)
	FOREIGN KEY (MaXa) REFERENCES Xa(MaXa),
	FOREIGN KEY (MaLCS) REFERENCES LoaiCoSo(MaLCS) 
)
GO

Create Table SanPhamXuLyChatThai(
	MaSP varchar(20) primary key,
	TenSP nvarchar(50),
	LoaiSP varchar(20),
	HanSD date, 
	MaSX varchar(20), 
	MaKN varchar(20)

	FOREIGN KEY (MaSX) REFERENCES CoSoSanXuatSP(MaSX),
	FOREIGN KEY (MaKN) REFERENCES CoSoKhaoNghiemSP(MaKN)
)
GO

Create Table DongVat(
	MaDV varchar(20) primary key, 
	LoaiDV nvarchar(50)
)
GO

Create Table DieuKienChanNuoi(
	MaDK varchar(20) primary key,
	MotaDK nvarchar(500)
)
GO

Create Table CoSoCapGCN (
	MaCSGCN varchar(20) primary key,
	TenCSGCN nvarchar(50),
	SoDT char(10),
	MaXa varchar(20),
	MaLCS varchar(20)

	FOREIGN KEY (MaXa) REFERENCES Xa(MaXa),
	FOREIGN KEY (MaLCS) REFERENCES LoaiCoSo(MaLCS)
)
GO

Create Table GiayChungNhan(
	MaGCN varchar(20) primary key,
	TenGCN nvarchar(50),
	MotaGCN nvarchar(500),
	MaCSGCN varchar(20)

	FOREIGN KEY (MaCSGCN) REFERENCES CoSoCapGCN(MaCSGCN)
)
GO

Create Table ToChucCaNhan(
	MaTCCN varchar(20) primary key,
	TenTCCN nvarchar(50),
	LoaiTCCN varchar(20),
	SoDT char(10)
)
GO

Create Table ThongKeChanNuoi(
	MaTK varchar(20) primary key, 
	SoLuongCNNL int,
	NgayTK date,
)
GO

Create Table Dichbenh(
	MaDB varchar(20) primary key,
	TenDB nvarchar(50),
	CachXyLy nvarchar(100),
	MucNguyHiem int,
)
GO

Create Table CoSoChanNuoi(
	MaCN varchar(20) primary key,
	TenCN nvarchar(50),
	MaDV varchar(20),
	SoLuongDV int,
	SoDT char(10),
	MaXa varchar(20),
	MaTCCN varchar(20),
	MaDK varchar(20),
	MaDB varchar(20),
	MaGCN varchar(20),
	MaLCS varchar(20),

	FOREIGN KEY (MaXa) REFERENCES Xa(MaXa),
	FOREIGN KEY (MaDV) REFERENCES DongVat(MaDV),
	FOREIGN KEY (MaTCCN) REFERENCES ToChucCaNhan(MaTCCN),
	FOREIGN KEY (MaDK) REFERENCES DieuKienChanNuoi(MaDK),
	FOREIGN KEY (MaDB) REFERENCES DichBenh(MaDB),
	FOREIGN KEY (MaGCN) REFERENCES GiayChungNhan(MaGCN),
	FOREIGN KEY (MaLCS) REFERENCES LoaiCoSo(MaLCS),	
)
GO

Create Table CoSoCheBien(
	MaCB varchar(20) primary key,
	TenCB nvarchar(50),
	MaDV varchar(20),
	SoLuongDV int,
	SoDT char(10),
	MaXa varchar(20),
	MaTCCN varchar(20),
	MaDK varchar(20),
	MaDB varchar(20),
	MaGCN varchar(20),
	MaLCS varchar(20),

	FOREIGN KEY (MaXa) REFERENCES Xa(MaXa),
	FOREIGN KEY (MaDV) REFERENCES DongVat(MaDV),
	FOREIGN KEY (MaTCCN) REFERENCES ToChucCaNhan(MaTCCN),
	FOREIGN KEY (MaDK) REFERENCES DieuKienChanNuoi(MaDK),
	FOREIGN KEY (MaDB) REFERENCES DichBenh(MaDB),
	FOREIGN KEY (MaGCN) REFERENCES GiayChungNhan(MaGCN),
	FOREIGN KEY (MaLCS) REFERENCES LoaiCoSo(MaLCS),	
)
GO

Create Table VungAnToan(
	MaVAT varchar(20) primary key,
	TenVAT nvarchar(50),
	--MaCN varchar(20),
	MaDB varchar(20)

	--FOREIGN KEY (MaCN) REFERENCES CoSoChanNuoi(MaCN),
	FOREIGN KEY (MaDB) REFERENCES DichBenh(MaDB)
)
GO

Create Table ChiCucThuY (
	MaCC varchar(20) primary key, 
	TenCC nvarchar(50),
	SoDT char(10),
	MaXa varchar(20),
	MaLCS varchar(20)

	FOREIGN KEY (MaLCS) REFERENCES LoaiCoSo(MaLCS),
	FOREIGN KEY (MaXa) REFERENCES Xa(MaXa)
)
GO

Create Table Thuoc(
	MaT varchar(20) primary key,
	TenT nvarchar(50),
	MaDV varchar(20),
	NgaySX date,
	HanSD date

	FOREIGN KEY (MaDV) REFERENCES DongVat(MaDV)
)
GO

Create Table DaiLyBanThuoc(
	MaDL varchar(20) primary key, 
	TenDL nvarchar(50),
	SoDT char(10),
	MaXa varchar(20),
	MaLCS varchar(20)
	FOREIGN KEY (MaLCS) REFERENCES LoaiCoSo(MaLCS),
	FOREIGN KEY (MaXa) REFERENCES Xa(MaXa)
)
GO

Create Table QuanLyDaiLy(
	MaDL varchar(20),
	MaT varchar(20)
	FOREIGN KEY (MaDL) REFERENCES DaiLyBanThuoc(MaDL),
	FOREIGN KEY (MaT) REFERENCES Thuoc(MaT)
)
GO

Create Table KhuTamGiu(
	MaKhu varchar(20) primary key,
	TenKhu nvarchar(50),
	MaDV varchar(20),
	SoLuong int,
	MaXa varchar(20),
	NgayTG date
	FOREIGN KEY (MaDV) REFERENCES DongVat(MaDV),
	FOREIGN KEY (MaXa) REFERENCES Xa(MaXa)
)
GO

Create Table CoSoGietMo (
	MaGM varchar(20) primary key,
	TenGM nvarchar(50),
	SoDT char(10),
	MaLCS varchar(20),
	MaXa varchar(20)
	FOREIGN KEY (MaLCS) REFERENCES LoaiCoSo(MaLCS),
	FOREIGN KEY (MaXa) REFERENCES Xa(MaXa)
)
GO