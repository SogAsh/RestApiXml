CREATE OR REPLACE VIEW public.question AS 
 SELECT wn.value,
    ws.id_name,
    ws.param_name
   FROM object_params wn
     LEFT JOIN param_type ws ON ws.id = wn.param_id
     WHERE ws.id_name = 'PT_NAME' 
     AND ws.param_name = 'Имя';

ALTER TABLE public.question
  OWNER TO postgres;