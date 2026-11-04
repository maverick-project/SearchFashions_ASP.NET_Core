CREATE TABLE fashions (
    id INT PRIMARY KEY,                             -- CSVのIDをそのまま登録
    gender VARCHAR(50),                            -- 例: Men, Women 等
    category VARCHAR(50),                       -- 例: Topwear, Bottomwear 等
    articleType VARCHAR(50),                     -- 商品の種類
    season VARCHAR(50),                            -- 例: Fall, Summer 等
    year INT,                                       -- 例: 2011 等
    usage VARCHAR(50),                             -- 例: Casual, Sports 等
    productDisplayName VARCHAR(100),               -- 商品名
    imagePath VARCHAR(100)                         -- 画像ファイルのパスやURL
);
