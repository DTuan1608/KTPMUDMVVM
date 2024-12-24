INSERT INTO Tinh (MaTinh, TenTinh)
VALUES 
('T01', 'Hà Nội'),
('T02', 'Hồ Chí Minh'),
('T03', 'Đà Nẵng'),
('T04', 'Hải Phòng'),
('T05', 'Cần Thơ'),
('T06', 'Bắc Ninh'),
('T07', 'Thanh Hóa'),
('T08', 'Nghệ An'),
('T09', 'Quảng Ninh'),
('T10', 'Thừa Thiên Huế');
GO

INSERT INTO Huyen (MaHuyen, TenHuyen, MaTinh)
VALUES 
('H01', 'Huyện Ba Vì', 'T01'),
('H02', 'Huyện Chương Mỹ', 'T02'),
('H03', 'Huyện Đan Phượng', 'T03'),
('H04', 'Huyện Đông Anh', 'T04'),
('H05', 'Huyện Gia Lâm', 'T05'),
('H06', 'Huyện Hoài Đức', 'T06'),
('H07', 'Huyện Mê Linh', 'T07'),
('H08', 'Huyện Mỹ Đức', 'T08'),
('H09', 'Huyện Phú Xuyên', 'T09'),
('H10', 'Huyện  Phúc Thọ', 'T010');
GO


INSERT INTO Xa (MaXa, TenXa, MaHuyen) 
VALUES 
('XA001', 'Xã Hòa Bình', 'H01'),
('XA002', 'Xã Tân Lập', 'H02'),
('XA003', 'Xã Phú Nhuận', 'H03'),
('XA004', 'Xã Hòa Phong', 'H04'),
('XA005', 'Xã Vĩnh Thạnh', 'H05'),
('XA006', 'Xã Mỹ Phước', 'H06'),
('XA007', 'Xã Bình Minh', 'H07'),
('XA008', 'Xã Xuân Phong', 'H08'),
('XA009', 'Xã Thái Hòa', 'H09'),
('XA010', 'Xã Tân Hưng', 'H010');
GO

INSERT INTO Nguoi_dung ( MaND,TenDN,MatKhau,TenHienThi,MaXa)
VALUES 
('ND001', 'user1', '123456', 'Nguyễn Văn A','XA001'),
('ND002', 'user2', 'abcdef', 'Trần Thị B','XA002'),
('ND003', 'user3', 'matkhau123', 'Lê Văn C','XA003'),
('ND004', 'user4', 'qwerty', 'Phạm Thị D','XA004'),
('ND005', 'user5', 'password', 'Ngô Thị E','XA005'),
('ND006', 'user6', 'zxcvbn', 'Vũ Văn F','XA006'),
('ND007', 'user7', '123qwe', 'Đỗ Thị G','XA007'),
('ND008', 'user8', 'iloveyou', 'Hoàng Văn H','XA008'),
('ND009', 'user9', '1a2b3c', 'Nguyễn Thị I','XA009'),
('ND010', 'user10', 'letmein', 'Trần Văn K','XA010');
GO

INSERT INTO LoaiCoSo (MaLCS, TenLCS)
VALUES 
    ('CSCN', 'Cơ sở chăn nuôi'),
    ('CSCB', 'Cơ sở chế biến'),
    ('CCTY', 'Chi cục thú y'),
    ('CSXS', 'Cơ sở sản xuất sản phẩm XLCT'),
    ('CSKN', 'Cơ sở khảo nghiệm sản phẩm XLCT'),
    ('DLBT', 'Đại lý bán thuốc'),
    ('CSCGCN', 'Cơ sở cấp giấy chứng nhận'),
    ('CSGM', 'Cơ sở giết mổ');
GO

INSERT INTO CoSoSanXuatSP(MaSX,TenSX,SoDT,MaXa,MaLCS) 
VALUES 
('SX01', 'Cơ sở sản xuất Hà Nội', '0241111222','XA001','CCSX'),
('SX02', 'Cơ sở sản xuất TP. Hồ Chí Minh', '0282222333','XA002','CCSX'),
('SX03', 'Cơ sở sản xuất Đà Nẵng', '02363333444','XA003','CCSX'),
('SX04', 'Cơ sở sản xuất Hải Phòng', '02254444555','XA004','CCSX'),
('SX05', 'Cơ sở sản xuất Cần Thơ', '02925555666','XA005','CCSX'),
('SX06', 'Cơ sở sản xuất Nghệ An', '02386666777','XA006','CCSX'),
('SX07', 'Cơ sở sản xuất Thanh Hóa', '02377777888','XA007','CCSX'),
('SX08', 'Cơ sở sản xuất Bình Dương', '02748888999','XA008','CCSX'),
('SX09', 'Cơ sở sản xuất Đồng Nai', '02519999000','XA009','CCSX'),
('SX10', 'Cơ sở sản xuất Khánh Hòa', '02580000111','XA010','CCSX');
GO

INSERT INTO CoSoKhaoNghiemSP(MaKN,TenKN,SoDT,MaXa,MaLCS) 
VALUES 
('KN01', 'Cơ sở khảo nghiệm Hà Nội', '0241122556','XA001','CSKN'),
('KN02', 'Cơ sở khảo nghiệm TP. Hồ Chí Minh', '0282233667','XA002','CSKN'),
('KN03', 'Cơ sở khảo nghiệm Đà Nẵng', '02363344778','XA003','CSKN'),
('KN04', 'Cơ sở khảo nghiệm Hải Phòng', '02254455889','XA004','CSKN'),
('KN05', 'Cơ sở khảo nghiệm Cần Thơ', '02925566990','XA005','CSKN'),
('KN06', 'Cơ sở khảo nghiệm Nghệ An', '02386677001','XA006','CSKN'),
('KN07', 'Cơ sở khảo nghiệm Thanh Hóa', '02377788112','XA007','CSKN'),
('KN08', 'Cơ sở khảo nghiệm Bình Dương', '02748899223','XA008','CSKN'),
('KN09', 'Cơ sở khảo nghiệm Đồng Nai', '02519900334','XA009','CSKN'),
('KN10', 'Cơ sở khảo nghiệm Khánh Hòa', '02580011445','XA010','CSKN');
GO
INSERT INTO SanPhamXuLyChatThai(MaSP, TenSP, LoaiSP, HanSD, MaSX,MaKN)
VALUES 
('SP001', 'Chế phẩm xử lý nước thải A', 'Vi sinh', '2025-12-31', 'CSX001','KN01'),
('SP002', 'Hóa chất khử trùng B', 'Hóa chất', '2026-06-15', 'CSX002','KN02'),
('SP003', 'Thiết bị lọc nước C', 'Thiết bị', '2027-09-10', 'CSX003','KN03'),
('SP004', 'Chế phẩm vi sinh D', 'Vi sinh', '2026-03-05', 'CSX004','KN04'),
('SP005', 'Dung dịch khử mùi E', 'Hóa chất', '2025-10-20', 'CSX005','KN05'),
('SP006', 'Máy ép bùn F', 'Thiết bị', '2028-01-15', 'CSX006','KN06'),
('SP007', 'Chế phẩm phân hủy hữu cơ G', 'Vi sinh', '2026-04-30', 'CSX007','KN07'),
('SP008', 'Hóa chất tẩy rửa H', 'Hóa chất', '2025-08-10', 'CSX008','KN08'),
('SP009', 'Bộ lọc khí I', 'Thiết bị', '2027-02-18', 'CSX009','KN09'),
('SP010', 'Chế phẩm xử lý bùn thải J', 'Vi sinh', '2026-11-11', 'CSX010','KN10');
GO
INSERT INTO DongVat (MaDV, LoaiDV ) 
VALUES 
('DV01', 'Gia súc'),
('DV02', 'Gia cầm'),
('DV03', 'Thú cưng'),
('DV04', 'Thú hoang dã'),
('DV05', 'Gia súc'),
('DV06', 'Gia cầm'),
('DV07', 'Thú cưng'),
('DV08', 'Thú hoang dã'),
('DV09', 'Gia súc'),
('DV10', 'Gia cầm');
GO
INSERT INTO DieuKienChanNuoi (MaDK, MoTaDK) 
VALUES 
('DK01', 'Chuồng trại phải sạch sẽ, khô ráo và thông thoáng.'),
('DK02', 'Đảm bảo nguồn thức ăn đầy đủ, an toàn và phù hợp với từng loại vật nuôi.'),
('DK03', 'Nguồn nước uống phải sạch và luôn có sẵn cho vật nuôi.'),
('DK04', 'Vật nuôi phải được tiêm phòng đầy đủ và đúng thời gian quy định.'),
('DK05', 'Chăn nuôi phải đảm bảo không có mầm bệnh, không có sự xâm nhập của động vật hoang dã.'),
('DK06', 'Quản lý dịch bệnh chặt chẽ, có kế hoạch phòng ngừa và điều trị kịp thời.'),
('DK07', 'Vật nuôi phải được chăm sóc và theo dõi sức khỏe thường xuyên.'),
('DK08', 'Điều kiện môi trường phải phù hợp với từng loại vật nuôi (nhiệt độ, độ ẩm, ánh sáng).'),
('DK09', 'Có đủ các biện pháp bảo vệ an toàn cho người lao động trong quá trình chăn nuôi.'),
('DK10', 'Chăn nuôi phải tuân thủ quy định về bảo vệ môi trường và xử lý chất thải đúng cách.');
GO
Insert into CoSoCapGCN( MaCSGCN,TenCSGCN,SoDT, MaXa,MaLCS )
values 
('001', 'Sở Nông nghiệp và Phát triển nông thôn Thành phố Hà Nội','Thành phố Hà Nội','04 1234 5678','XA001','CSCGCN'),
('002', 'Sở Nông nghiệp và Phát triển nông thôn Thành phố Hồ Chí Minh ','Thành phố Hồ Chí Minh ','08 9876 543','XA002','CSCGCN'),
('003', 'Sở Nông nghiệp và Phát triển nông thôn Đà Nẵng ','Đà Nẵng','0523456789','XA003','CSCGCN'),
('004', 'Sở Nông nghiệp và Phát triển nông thôn Hải Phòng ','Hải Phòng','03 3456 7890','XA004','CSCGCN'),
('005', 'Sở Nông nghiệp và Phát triển nông thôn Cần Thơ','Cần Thơ','09 4567 8901','XA005','CSCGCN');
GO
INSERT INTO GiayChungNhan (MaGCN, TenGCN, MoTaGCN, MaCoSoGCN) 
VALUES 
('GCN01', 'Giấy chứng nhận vệ sinh an toàn thực phẩm', 'Chứng nhận cơ sở đáp ứng tiêu chuẩn vệ sinh an toàn thực phẩm.', 'CS01'),
('GCN02', 'Giấy chứng nhận kiểm dịch động vật', 'Chứng nhận động vật đã kiểm dịch đầy đủ.', 'CS02'),
('GCN03', 'Giấy chứng nhận đủ điều kiện sản xuất', 'Chứng nhận cơ sở có đủ điều kiện để sản xuất sản phẩm chăn nuôi.', 'CS03'),
('GCN04', 'Giấy chứng nhận xuất xứ sản phẩm', 'Xác nhận nguồn gốc xuất xứ của sản phẩm từ trang trại.', 'CS04'),
('GCN05', 'Giấy chứng nhận thực hành chăn nuôi tốt (GAHP)', 'Chứng nhận thực hành chăn nuôi tốt theo tiêu chuẩn GAHP.', 'CS05'),
('GCN06', 'Giấy chứng nhận phòng chống dịch bệnh', 'Chứng nhận cơ sở có đủ biện pháp phòng chống dịch bệnh.', 'CS06'),
('GCN07', 'Giấy chứng nhận đủ điều kiện vận chuyển', 'Chứng nhận phương tiện vận chuyển đạt tiêu chuẩn vận chuyển động vật.', 'CS07'),
('GCN08', 'Giấy chứng nhận an toàn dịch bệnh', 'Chứng nhận cơ sở không có dịch bệnh hoặc dịch bệnh đã được kiểm soát.', 'CS08'),
('GCN09', 'Giấy chứng nhận kiểm tra chất lượng sản phẩm', 'Xác nhận sản phẩm đạt chất lượng theo tiêu chuẩn kiểm định.', 'CS09'),
('GCN10', 'Giấy chứng nhận đủ điều kiện chăn nuôi', 'Chứng nhận cơ sở đáp ứng các điều kiện chăn nuôi theo quy định.', 'CS10');
GO
INSERT INTO ToChucCaNhan (MaTCCN, TenTCCN, LoaiTCCN, SoDT) 
VALUES 
('TC001', 'Công ty TNHH ABC', 'Tổ chức', '0912345678'),
('TC002', 'Công ty CP Xây dựng DEF', 'Tổ chức', '0987654321'),
('TC003', 'Nguyễn Văn A', 'Cá nhân', '0934567890'),
('TC004', 'Lê Thị B', 'Cá nhân', '0945678901'),
('TC005', 'Công ty TNHH Sản xuất GHI', 'Tổ chức', '0923456789'),
('TC006', 'Trần Văn C', 'Cá nhân', '0976543210'),
('TC007', 'Công ty CP Thương mại JKL', 'Tổ chức', '0911223344'),
('TC008', 'Phạm Thị D', 'Cá nhân', '0966677889'),
('TC009', 'Doanh nghiệp tư nhân MNO', 'Tổ chức', '0955512345'),
('TC010', 'Nguyễn Thị E', 'Cá nhân', '0933221100');
GO
INSERT INTO ThongKeChanNuoi (MaTK, SoLuongCNNL, NgayTK)
VALUES 
('TK001', 250, '2024-01-15'),
('TK002', 500, '2024-02-20'),
('TK003', 300, '2024-03-10'),
('TK004', 600, '2024-04-05'),
('TK005', 700, '2024-05-25'),
('TK006', 450, '2024-06-12'),
('TK007', 800, '2024-07-30'),
('TK008', 350, '2024-08-18'),
('TK009', 900, '2024-09-09'),
('TK010', 1000, '2024-10-02');
GO
INSERT INTO DichBenh(MaDB, TenDB, CachXuLy, MucNguyHiem)
VALUES 
('DB01', 'Dịch tả lợn châu Phi', 'Cách ly khu vực bị nhiễm, tiêu hủy lợn nhiễm bệnh, khử trùng trang trại.', 'Rất nghiêm trọng'),
('DB02', 'Lở mồm long móng', 'Tiêm phòng, cách ly động vật bị nhiễm, khử trùng các khu vực bị nhiễm.', 'Nghiêm trọng'),
('DB03', 'Cúm gia cầm H5N1', 'Tiêu hủy gia cầm bị nhiễm bệnh, khử trùng trang trại, tiêm phòng phòng ngừa.', 'Rất nghiêm trọng'),
('DB04', 'Bệnh tụ huyết trùng', 'Tiêm phòng, sử dụng kháng sinh theo chỉ định của bác sĩ thú y.', 'Trung bình'),
('DB05', 'Bệnh giun tròn ký sinh', 'Tẩy giun định kỳ cho động vật, vệ sinh chuồng trại, cung cấp thức ăn sạch.', 'Thấp'),
('DB06', 'Bệnh viêm phổi trên bò', 'Điều trị kháng sinh, tăng cường sức đề kháng cho bò, giữ môi trường thông thoáng.', 'Trung bình'),
('DB07', 'Dại trên chó mèo', 'Tiêm phòng dại định kỳ, cách ly động vật bị nghi ngờ nhiễm bệnh.', 'Cao'),
('DB08', 'Bệnh Marek trên gà', 'Tiêm phòng vaccine Marek, vệ sinh chuồng trại sạch sẽ.', 'Cao'),
('DB09', 'Bệnh tiêu chảy cấp (PED) trên heo', 'Vệ sinh chuồng trại, tiêm phòng, cung cấp dinh dưỡng đầy đủ.', 'Nghiêm trọng'),
('DB10', 'Bệnh Newcastle trên gia cầm', 'Tiêm phòng vaccine Newcastle, vệ sinh chuồng trại, khử trùng định kỳ.', 'Nghiêm trọng');
GO
INSERT INTO CoSoChanNuoi (MaCN, TenCN,MaDV,SoLuongDV, SoDT, MaXa, MaTCCN, MaDK, MaDB, MaGCN, MaLCS)
VALUES 
('CN001', 'Trang trại Hòa Phát','DV01','150', '0912345678','XA001','TC001','DK01','DB01','GCN01','CSCN'),
('CN002', 'Trang trại Gia Phúc','DV02','200', '0987654321','XA002','TC002','DK02','DB02','GCN02','CSCN'),
('CN003', 'Trang trại Đồng Tâm', 'DV03','120','0901234567','XA003','TC003','DK03','DB03','GCN03','CSCN'),
('CN004', 'Nông trại Phúc An','DV04','180', '0934567890','XA004','TC004','DK04','DB04','GCN04','CSCN'),
('CN005', 'Trang trại Bình Minh','DV05','250', '0976543210','XA005','TC005','DK05','DB05','GCN05','CSCN'),
('CN006', 'Nông trại Thái Hòa','DV06','300', '0923456789','XA006','TC006','DK06','DB06','GCN06','CSCN'),
('CN007', 'Trang trại Việt Phát','DV07','90', '0945678901','XA007','TC007','DK07','DB07','GCN07','CSCN'),
('CN008', 'Nông trại Ngọc Linh','DV08','75', '0888123456','XA008','TC008','DK08','DB08','GCN08','CSCN'),
('CN009', 'Trang trại An Bình','DV09','220', '0867890123','XA009','TC009','DK09','DB09','GCN09','CSCN'),
('CN010', 'Trang trại Thịnh Vượng','DV10','130', '0890987654','XA010','TC010','DK10','DB10','GCN10','CSCN');
GO
INSERT INTO CoSoCheBien (MaCB, TenCB, MaDV, SoLuongDV, SoDT, MaXa, MaTCCN, MaDK, MaDB, MaGCN, MaLCS) 
VALUES 
('CB001', 'Cơ sở chế biến thực phẩm ABC','DV01','150', '0912345678','XA001','TC001','DK01','DB01','GCN01','CSCB'),
('CB002', 'Nhà máy chế biến thủy sản Hưng Phát','DV02','200', '0987654321','XA002','TC002','DK02','DB02','GCN02','CSCB'),
('CB003', 'Xưởng chế biến nông sản Hoàng Long', 'DV03','120','0901234567','XA003','TC003','DK03','DB03','GCN03','CSCB'),
('CB004', 'Nhà máy chế biến sữa Thanh Đạt','DV04','180', '0934567890','XA004','TC004','DK04','DB04','GCN04','CSCB'),
('CB005', 'Công ty chế biến gia vị Đông Á','DV05','250', '0976543210','XA005','TC005','DK05','DB05','GCN05','CSCB'),
('CB006', 'Cơ sở sản xuất đồ uống An Phú','DV06','300', '0923456789','XA006','TC006','DK06','DB06','GCN06','CSCB'),
('CB007', 'Xưởng chế biến gỗ Hòa Bình','DV07','90', '0945678901','XA007','TC007','DK07','DB07','GCN07','CSCB'),
('CB008', 'Nhà máy chế biến thịt Tiến Phát','DV08','75', '0888123456','XA008','TC008','DK08','DB08','GCN08','CSCB'),
('CB009', 'Doanh nghiệp chế biến hạt điều Nam Việt','DV09','220', '0867890123','XA009','TC009','DK09','DB09','GCN09','CSCB'),
('CB010', 'Nhà máy chế biến đường mía Ngọc Linh','DV10','130', '0890987654','XA010','TC010','DK10','DB10','GCN10','CSCB');
GO
INSERT INTO VungAnToan (MaVAT, TenVAT,MaDB)
VALUES 
('VA001', 'Vùng an toàn 1 - Khu vực phía Bắc','DB01'),
('VA002', 'Vùng an toàn 2 - Khu công nghiệp phía Nam','DB02'),
('VA003', 'Vùng an toàn 3 - Trung tâm hành chính','DB03'),
('VA004', 'Vùng an toàn 4 - Khu vực dân cư phía Tây','DB04'),
('VA005', 'Vùng an toàn 5 - Nhà máy sản xuất A','DB05'),
('VA006', 'Vùng an toàn 6 - Tòa nhà văn phòng B','DB06'),
('VA007', 'Vùng an toàn 7 - Trung tâm thương mại C','DB07'),
('VA008', 'Vùng an toàn 8 - Hầm ngầm dưới lòng đất','DB08'),
('VA009', 'Vùng an toàn 9 - Kho chứa hàng D','DB09'),
('VA010', 'Vùng an toàn 10 - Khu vực trường học và bệnh viện','DB10');
GO
INSERT INTO ChiCucThuY (MaCC, TenCC, SoDT,MaXa,MaLCS)
VALUES 
('CC01', 'Chi cục Thú y Hà Nội', '0241234567','XA001','CCTY'),
('CC02', 'Chi cục Thú y TP. Hồ Chí Minh', '0282345678','XA002','CCTY'),
('CC03', 'Chi cục Thú y Đà Nẵng', '0236345678','XA003','CCTY'),
('CC04', 'Chi cục Thú y Hải Phòng', '0225456789','XA004','CCTY'),
('CC05', 'Chi cục Thú y Cần Thơ', '0292567890','XA005','CCTY'),
('CC06', 'Chi cục Thú y Nghệ An', '0238678901','XA006','CCTY'),
('CC07', 'Chi cục Thú y Thanh Hóa', '0237789012','XA007','CCTY'),
('CC08', 'Chi cục Thú y Bình Dương', '0274890123','XA008','CCTY'),
('CC09', 'Chi cục Thú y Đồng Nai', '0251901234','XA009','CCTY'),
('CC10', 'Chi cục Thú y Khánh Hòa', '0258012345','XA010','CCTY');
GO
INSERT INTO Thuoc (MaT,TenT, MaDV, NgaySX, HanSD)
VALUES 
('TH001', 'Thuốc giảm đau Paracetamol', 'DV01', '2023-01-15', '2025-01-15'),
('TH002', 'Thuốc kháng sinh Amoxicillin', 'DV02', '2023-02-20', '2025-02-20'),
('TH003', 'Thuốc kháng viêm Ibuprofen', 'DV03', '2023-03-10', '2025-03-10'),
('TH004', 'Thuốc hạ sốt Aspirin', 'DV04', '2023-04-05', '2025-04-05'),
('TH005', 'Thuốc chống dị ứng Cetirizine', 'DV05', '2023-05-25', '2025-05-25'),
('TH006', 'Thuốc kháng nấm Fluconazole', 'DV06', '2023-06-12', '2025-06-12'),
('TH007', 'Thuốc an thần Diazepam', 'DV07', '2023-07-30', '2025-07-30'),
('TH008', 'Thuốc trị ho Dextromethorphan', 'DV08', '2023-08-18', '2025-08-18'),
('TH009', 'Thuốc chữa viêm loét dạ dày Omeprazole', 'DV09', '2023-09-09', '2025-09-09'),
('TH010', 'Thuốc chữa táo bón Bisacodyl', 'DV10', '2023-10-02', '2025-10-02');
GO
INSERT INTO DaiLyBanThuoc(MaDL, TenDL, SoDT,MaXa,MaLCS) 
VALUES 
('DL01', 'Đại lý bán thuốc Hà Nội', '0241111222','XA001','DLBT'),
('DL02', 'Đại lý bán thuốc TP. Hồ Chí Minh', '0282222333','XA002','DLBT'),
('DL03', 'Đại lý bán thuốc Đà Nẵng', '02363333444','XA003','DLBT'),
('DL04', 'Đại lý bán thuốc Hải Phòng', '02254444555','XA004','DLBT'),
('DL05', 'Đại lý bán thuốc Cần Thơ', '02925555666','XA005','DLBT'),
('DL06', 'Đại lý bán thuốc Nghệ An', '02386666777','XA006','DLBT'),
('DL07', 'Đại lý bán thuốc Thanh Hóa', '02377777888','XA007','DLBT'),
('DL08', 'Đại lý bán thuốc Bình Dương', '02748888999','XA008','DLBT'),
('DL09', 'Đại lý bán thuốc Đồng Nai', '02519999000','XA009','DLBT'),
('DL10', 'Đại lý bán thuốc Khánh Hòa', '02580000111','XA010','DLBT');
GO
INSERT INTO QuanLyDaiLy(MaDL, MaT)
VALUES
('DL01','TH001')
('DL02','TH002')
('DL03','TH003')
('DL04','TH004')
('DL05','TH005')
('DL06','TH006')
('DL07','TH007')
('DL08','TH008')
('DL09','TH009')
('DL10','TH010')
GO
INSERT INTO KhuTamGiu(MaKhu, TenKhu,MaDV, SoLuong,Maxa, NgayTG) 
VALUES 
('K01', 'Khu tạm giữ A', 'DV01', '15','XA001', '2024-01-10'),
('K02', 'Khu tạm giữ B', 'DV02', '20','XA002', '2024-01-15'),
('K03', 'Khu tạm giữ C', 'DV03', '25','XA003', '2024-02-01'),
('K04', 'Khu tạm giữ D', 'DV04', '18','XA004', '2024-02-10'),
('K05', 'Khu tạm giữ E', 'DV05', '30','XA005', '2024-02-20'),
('K06', 'Khu tạm giữ F', 'DV06', '22','XA006', '2024-03-05'),
('K07', 'Khu tạm giữ G', 'DV07', '27', 'XA007','2024-03-10'),
('K08', 'Khu tạm giữ H', 'DV08', '12','XA008', '2024-03-20'),
('K09', 'Khu tạm giữ I', 'DV09', '19','XA009', '2024-04-01'),
('K10', 'Khu tạm giữ J', 'DV10', '16','XA010', '2024-04-10');
GO
INSERT INTO CoSoGietMo(MaGM,TenGM,SoDT,MaXa,MaLCS)
VALUES 
('CS01', 'Cơ sở giết mổ Hà Nội', '0241122334','XA001','CSGM'),
('CS02', 'Cơ sở giết mổ TP. Hồ Chí Minh', '0282233445','XA002','CSGM'),
('CS03', 'Cơ sở giết mổ Đà Nẵng', '02363344556','XA003','CSGM'),
('CS04', 'Cơ sở giết mổ Hải Phòng', '02254455667','XA004','CSGM'),
('CS05', 'Cơ sở giết mổ Cần Thơ', '02925566778','XA005','CSGM'),
('CS06', 'Cơ sở giết mổ Nghệ An', '02386677889','XA006','CSGM'),
('CS07', 'Cơ sở giết mổ Thanh Hóa', '02377788990','XA007','CSGM'),
('CS08', 'Cơ sở giết mổ Bình Dương', '02748899001','XA008','CSGM'),
('CS09', 'Cơ sở giết mổ Đồng Nai', '02519900112','XA009','CSGM'),
('CS10', 'Cơ sở giết mổ Khánh Hòa', '02580011223','XA010','CSGM')
GO