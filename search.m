%search.m - Funkcija vrne dokumente, kateri so še v meji naše tolerance.
%Prvi argument - direktorji, kjer so shranjeni podatki
%Drugi argument - naša toleranca

args = argv();
load_path = args{1};
min_cos = str2num(args{2})/100;

load(load_path);

q = zeros(length(unique_words), 1);
for i = 3:length(args)
	q += ismember(unique_words, lower(args{i}(isalnum(args{i}))));
end

q2 = q' * U * inv(S);
cos = (V * q2') ./ (sqrt(sum(q2.^2)) * sqrt(sum(V.^2, 2)));
relevant_docs = sortrows([(1:number_of_docs)', cos](cos > min_cos, :), -2);
doc_names = file_names(relevant_docs(:, 1));

result = "\n";
for i = 1:size(relevant_docs, 1)
	result = [result, "\n", num2str(relevant_docs(i, 2)), "\t", doc_names{i}];
end
result
