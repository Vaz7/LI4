Use Li4
SELECT * FROM Li4.Utilizador
SELECT * FROM Li4.leilao
SELECT * FROM Li4.Quadro
SELECT * FROM Li4.licitacao
SELECT * FROM Li4.Morada


SELECT l.idLeilao, l.nome, l.data_inicio, l.data_fim, l.estado, l.valor_base, l.pago,
   l.Utilizador_email
FROM Li4.leilao l
INNER JOIN Li4.licitacao lic ON l.idLeilao = lic.leilao_idLeilao
WHERE l.estado = 1 
  AND l.pago = 0
  AND lic.Utilizador_email = 'asd@asd.asd'
  AND lic.valor = (SELECT MAX(valor) FROM Li4.licitacao WHERE leilao_idLeilao = l.idLeilao);


UPDATE Li4.leilao
SET estado = CASE 
                WHEN data_fim < GETDATE() THEN 1
                ELSE 0
             END
WHERE data_fim < GETDATE() AND estado = 0
