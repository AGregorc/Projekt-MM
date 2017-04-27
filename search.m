args = argv();
base_path = args{1};

load([base_path, 'generated_search_data.mat']);

q = zeros(length(unique_words), 1);
for i = 2:length(args)
	q = q | ismember(unique_words, args{i});
end

q2 = q' * U * inv(S);
cos = (V * q2') ./ (sqrt(sum(q2.^2)) * sqrt(sum(V.^2, 2)));
relevant_docs = sortrows([(1:number_of_docs)', cos](cos > 0.8, :), -2);
doc_names = file_names(relevant_docs(:, 1));

result = '';
for i = 1:length(relevant_docs)
	result = [result; num2str(relevant_docs(i, 2)), "\t", doc_names{i}];
end
result
