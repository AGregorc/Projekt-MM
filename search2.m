%function result = search(search_str)
  
  search_arguments = argv();%dobi vse argumente
  stBesed = str2num(search_arguments{1});%prvi argument je dolzina iskalnega niza
  search_str=[]; 
  for r = 2:1+stBesed;
	search_str = strcat(search_str,cstrcat(' ',search_arguments{r}));
  pot = search_arguments{2+stBesed};%zadnji argument je pot do podatkov .mat
  end
  search_str;
  pot;
  
  
  load(pot);
  
  search_words = strsplit(search_str);
  q = zeros(length(unique_words), 1);
  for i=1:length(search_words)
    q = q | ismember(unique_words, search_words{i});
  end
  
  q2 = q' * U * inv(S);
  cos = (V * q2') ./ (sqrt(sum(q2.^2)) * sqrt(sum(V.^2, 2)));
  relevant_docs = sortrows([(1:number_of_docs)', cos](cos > 0.8, :), -2);
  doc_names = file_names(relevant_docs(:, 1));
  cos_values = relevant_docs(:, 2);
  suc="oja"
  [doc_names, cos_values]
  
%end
