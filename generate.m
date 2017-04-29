%generate.m - Prebere datoteke, jih predela in shrani pridobljene podatke.
%Prvi argument - diraktorij datotek
%Drugi argument - metoda: izboljšana metoda = 'a', navadna metoda = 'b'
%Tretji argument - diraktorij za shranjevanje rezultatov

args = argv();
docs_dir = args{1};
mode = args{2};
save_path = args{3};

file_names = readdir(docs_dir)(3:end);
number_of_docs = length(file_names);
all_words = [];
num_of_words_in_docs = zeros(1, number_of_docs);
for i = 1:number_of_docs
	doc = textread([docs_dir, filesep, file_names{i}], '%s');
	for j = 1:length(doc)
		doc{j} = lower(doc{j}(isalnum(doc{j})));
	end
	all_words = [all_words; doc];
	num_of_words_in_docs(i) = length(doc);
end

[unique_words, ~, numbers] = unique(all_words);
all_possible_numbers = (1:length(unique_words))';
f = zeros(length(unique_words), number_of_docs);
doc_end = 0;
for i = 1:number_of_docs
	doc_start = doc_end + 1;
	doc_end = doc_start + num_of_words_in_docs(i) - 1;
	f(:, i) = histc(numbers(doc_start:doc_end, 1), all_possible_numbers);
end

if(mode == 'a')
	L = log(f + 1);
	gf = histc(numbers, all_possible_numbers);
	p = f ./ gf;
	plogp = p .* log(p);
	plogp(isnan(plogp)) = 0;
	G = 1 - sum(plogp/number_of_docs, 2);
	a = L .* G;
else
	a = f;
end

svd_errors = zeros(rank(a), 1);
for i = 1:length(svd_errors)
	[U, S, V] = svds(a, i);
	svd_errors(i) = norm(U*S*V' - a, 'inf');
end

left_limit = 1;
right_limit = length(svd_errors);
while(right_limit - left_limit > 2)
	left = round(left_limit + (right_limit - left_limit) / 3);
	right = round(right_limit - (right_limit - left_limit) / 3);
  
	[~, ~, r1_left] = ols(svd_errors(1:left), [(1:left)', ones(left, 1)]);
	[~, ~, r2_left] = ols(svd_errors(left:length(svd_errors)), 
      [(left:length(svd_errors))', ones(1+length(svd_errors)-left, 1)]);
	
  
  [~, ~, r1_right] = ols(svd_errors(1:right), [(1:right)', ones(right)]);
	[~, ~, r2_right] = ols(svd_errors(right:length(svd_errors)), 
      [(right:length(svd_errors))', ones(1+length(svd_errors)-right, 1)]);
  
  
	if(mean([r1_left; r2_left].^2) < mean([r1_right; r2_right].^2))
		right_limit = right;
	else
		left_limit = left;
	end
end
k = round((right_limit + left_limit) / 2)

%x = [1; length(svd_errors)];
%[b, ~, ~] = ols(svd_errors(1:k), [(1:k)', ones(k, 1)]);
%y1 = b(1)*x + b(2);
%[b, ~, ~] = ols(svd_errors(k:length(svd_errors)), [(k:length(svd_errors))', ones(1+length(svd_errors)-k, 1)]);
%y2 = b(1)*x + b(2);
%plot((1:length(svd_errors))', svd_errors, x, y1, x, y2);

[U, S, V] = svds(a, k);

save(save_path, 'file_names', 'number_of_docs', 'unique_words', 'U', 'S', 'V');
