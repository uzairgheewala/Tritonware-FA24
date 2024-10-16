import tkinter as tk
from tkinter import ttk, messagebox
import json
from tkinter.scrolledtext import ScrolledText

class DialogueEntry:
    def __init__(self, root):
        self.root = root
        self.root.title("Dialogue Entry")
        
        self.dialogue = {
            "characterName": "",
            "sentences": []
        }
        
        self.current_sentence = None   

        self.create_widgets()
        self.update_json_display()

    def create_widgets(self):
        # Frame for Character Name
        char_frame = ttk.Frame(self.root, padding="10")
        char_frame.grid(row=0, column=0, sticky="W")
        
        ttk.Label(char_frame, text="Character Name:").grid(row=0, column=0, sticky="W")
        self.char_entry = ttk.Entry(char_frame, width=30)
        self.char_entry.grid(row=0, column=1, padx=5)
        self.char_entry.bind("<FocusOut>", self.set_character_name)
        
        # Frame for Sentences
        sentence_frame = ttk.LabelFrame(self.root, text="Add Sentence", padding="10")
        sentence_frame.grid(row=1, column=0, padx=10, pady=5, sticky="EW")
        
        ttk.Label(sentence_frame, text="Sentence Text:").grid(row=0, column=0, sticky="W")
        self.sentence_entry = ttk.Entry(sentence_frame, width=50)
        self.sentence_entry.grid(row=0, column=1, padx=5, pady=2)
        
        add_sentence_btn = ttk.Button(sentence_frame, text="Add Sentence", command=self.add_sentence)
        add_sentence_btn.grid(row=0, column=2, padx=5)
        
        # Frame for Choices
        choice_frame = ttk.LabelFrame(self.root, text="Add Choice", padding="10")
        choice_frame.grid(row=2, column=0, padx=10, pady=5, sticky="EW")
        
        ttk.Label(choice_frame, text="Choice Text:").grid(row=0, column=0, sticky="W")
        self.choice_entry = ttk.Entry(choice_frame, width=50)
        self.choice_entry.grid(row=0, column=1, padx=5, pady=2)
        
        ttk.Label(choice_frame, text="Next Dialogue Character:").grid(row=1, column=0, sticky="W")
        self.next_char_entry = ttk.Entry(choice_frame, width=30)
        self.next_char_entry.grid(row=1, column=1, padx=5, pady=2)
        
        ttk.Label(choice_frame, text="Next Sentence Text:").grid(row=2, column=0, sticky="W")
        self.next_sentence_entry = ttk.Entry(choice_frame, width=50)
        self.next_sentence_entry.grid(row=2, column=1, padx=5, pady=2)
        
        add_choice_btn = ttk.Button(choice_frame, text="Add Choice", command=self.add_choice)
        add_choice_btn.grid(row=3, column=1, pady=5, sticky="E")
        
        # JSON Display
        json_frame = ttk.LabelFrame(self.root, text="JSON Output", padding="10")
        json_frame.grid(row=3, column=0, padx=10, pady=5, sticky="NSEW")
        
        self.json_display = ScrolledText(json_frame, width=80, height=20, state='disabled', bg="#f0f0f0")
        self.json_display.pack(fill="both", expand=True)
        
        self.root.grid_rowconfigure(3, weight=1)
        self.root.grid_columnconfigure(0, weight=1)

    def set_character_name(self, event):
        name = self.char_entry.get().strip()
        if name:
            self.dialogue["characterName"] = name
            self.update_json_display()
        else:
            messagebox.showwarning("Input Error", "Character name cannot be empty.")

    def add_sentence(self):
        text = self.sentence_entry.get().strip()
        if not text:
            messagebox.showwarning("Input Error", "Sentence text cannot be empty.")
            return
        sentence = {
            "text": text,
            "choices": []
        }
        self.dialogue["sentences"].append(sentence)
        self.current_sentence = sentence  # Set as current sentence for adding choices
        self.sentence_entry.delete(0, tk.END)
        self.update_json_display()
        messagebox.showinfo("Success", "Sentence added. Now you can add choices for this sentence.")

    def add_choice(self):
        if not self.current_sentence:
            messagebox.showwarning("No Sentence", "Please add a sentence before adding choices.")
            return
        choice_text = self.choice_entry.get().strip()
        next_char = self.next_char_entry.get().strip()
        next_sentence = self.next_sentence_entry.get().strip()
        
        if not choice_text or not next_char or not next_sentence:
            messagebox.showwarning("Input Error", "All choice fields must be filled.")
            return
        
        choice = {
            "choiceText": choice_text,
            "nextDialogue": {
                "characterName": next_char,
                "sentences": [
                    {
                        "text": next_sentence,
                        "choices": []
                    }
                ]
            }
        }
        
        self.current_sentence["choices"].append(choice)
        
        # Clear choice entries
        self.choice_entry.delete(0, tk.END)
        self.next_char_entry.delete(0, tk.END)
        self.next_sentence_entry.delete(0, tk.END)
        self.update_json_display()
        messagebox.showinfo("Success", "Choice added.")

    def update_json_display(self):
        self.json_display.configure(state='normal')

        pretty_json = json.dumps(self.dialogue, indent=4)
        with open('data.json', 'w') as f:
            json.dump(pretty_json, f)
        
        self.json_display.delete(1.0, tk.END)
        self.json_display.insert(tk.END, pretty_json)
        self.json_display.configure(state='disabled')

if __name__ == "__main__":
    root = tk.Tk()
    app = DialogueEntry(root)
    root.mainloop()