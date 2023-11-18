-- Populating Morada table
INSERT INTO Li4.Morada (rua, cidade, cod_postal, pais)
VALUES
    ('123 Main St', 'City1', '12345', 'Country1'),
    ('456 Oak St', 'City2', '67890', 'Country2'),
    ('789 Pine St', 'City3', '13579', 'Country3');

-- Populating Utilizador table
INSERT INTO Li4.Utilizador (email, nome, data_nascimento, NIF,password, Morada_idMorada)
VALUES
    ('user1@example.com', 'User 1', '1990-01-01', '123456789', 'cebola',1),
    ('user2@example.com', 'User 2', '1985-05-15', '987654321','cebola' ,2),
    ('user3@example.com', 'User 3', '2000-10-30', '111223344','cebola' ,3);

-- Populating Leilao table
INSERT INTO Li4.leilao (idLeilao, nome, data_inicio, data_fim, estado, valor_base, Utilizador_email)
VALUES
    (1, 'Leilao 1', '2023-01-01', '2023-02-01', 'Ativo', 100.00, 'user1@example.com'),
    (2, 'Leilao 2', '2023-02-01', '2023-03-01', 'Fechado', 150.00, 'user2@example.com'),
    (3, 'Leilao 3', '2023-03-01', '2023-04-01', 'Ativo', 200.00, 'user3@example.com');

-- Populating licitacao table
INSERT INTO Li4.licitacao (idlicitacao, data, valor)
VALUES
    (1, '2023-01-15', 120.00),
    (2, '2023-02-15', 160.00),
    (3, '2023-03-15', 220.00);

-- Populating utilizador_leilao table
INSERT INTO Li4.utilizador_leilao (Utilizador_email, Leilao_idLeilao, licitacao_idlicitacao)
VALUES
    ('user1@example.com', 1, 1),
    ('user2@example.com', 2, 2),
    ('user3@example.com', 3, 3);

-- Populating Quadro table
INSERT INTO Li4.Quadro (idQuadro, Título, Ano, Altura, Largura, Descricao, Moldura, Autor, Leilao_idLeilao)
VALUES
    (1, 'Quadro 1', '2000', 30.5, 40.5, 'Description 1', 1, 'Artist 1', 1),
    (2, 'Quadro 2', '2010', 25.0, 35.0, 'Description 2', 0, 'Artist 2', 2),
    (3, 'Quadro 3', '1995', 40.0, 50.0, 'Description 3', 1, 'Artist 3', 3);


-- SELECT * FROM Li4.Utilizador as u JOIN Li4.Morada as m ON m.idMorada = u.Morada_idMorada;
-- SELECT * FROM Li4.Morada;