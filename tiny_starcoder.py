from transformers import AutoModelForCausalLM, AutoTokenizer
import os
import pickle as pkl

predictions = []
references = []

checkpoint = "bigcode/tiny_starcoder_py"
device = "cpu"

tokenizer = AutoTokenizer.from_pretrained(checkpoint)
model = AutoModelForCausalLM.from_pretrained(checkpoint).to(device)

params = {
    'max_new_tokens': 128,
    'temperature': 0.2,
    'top_k': 50,
    'top_p': 0.1,
    'repetition_penalty': 1.17
}

directory = 'Samples'

for filename in os.listdir(directory):
    f = open(directory + '/' + filename, 'rb')
    data = pkl.load(f)
    f.close()

    input_text = "<fim_prefix>" + data[0] + "<fim_suffix>" + data[2] + "<fim_middle>"
    inputs = tokenizer.encode(input_text, return_tensors="pt").to(device)
    outputs = model.generate(inputs, pad_token_id=tokenizer.eos_token_id, **params)
    out = tokenizer.decode(outputs[0])
    out = out[out.find("<fim_middle>")+len("<fim_middle>"):-13]
    
    print(filename + ":")
    print("    Prediction - " + out)
    print("    Actual     - " + data[1] + "\n")
    predictions.append(out)
    references.append(data[1])

g = open('predictions.pkl', 'wb')
pkl.dump([predictions, references], g)
g.close()
