# tính loss
Giả sử cho 1 hàm bất kỳ đi qua 1 điểm, rồi tính toán khoảng cách các điểm còn lại so với hàm đi qua điểm ban đầu
# ipoch 
Tổng số ảnh cần phải tính loss
# batch
chia tổng số ảnh ra làm nhiều phần để tính loss , để chạy hết ipoch

				1, 16, 32, 64 ...
				tổng số ảnh = 6400 cái 6400/ 64 ra 1000 => suy ra 1 cần chạy 64 lần mới hoàn thành 1 ipoch
