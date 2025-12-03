CREATE TABLE test_items (
    id SERIAL PRIMARY KEY,
    name VARCHAR(200) NOT NULL,
    description TEXT,
    created_at TIMESTAMP NOT NULL DEFAULT NOW()
);

INSERT INTO test_items (name, description)
VALUES 
    ('テストアイテムA', 'これはテスト用のアイテムAです。'),
    ('テストアイテムB', '説明文B'),
    ('サンプルアイテムC', '詳細情報C');

SELECT * FROM test_items;