function [doc_names, cos_values] = search2(search_str)
  
  load('generated_search_data.mat');
  
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
  
end
