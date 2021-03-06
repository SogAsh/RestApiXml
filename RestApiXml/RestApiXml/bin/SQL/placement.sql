CREATE OR REPLACE VIEW public.placement AS 
 SELECT wn.value,
    ws.id_name,
    ws.param_name
   FROM object_params wn
     LEFT JOIN param_type ws ON ws.id = wn.param_id
     WHERE ws.id_name = 'PT_NAME' 
     AND ws.param_name = 'Идентификатор';

ALTER TABLE public.placement
  OWNER TO postgres;