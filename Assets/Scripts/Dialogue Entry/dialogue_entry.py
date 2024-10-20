import tkinter as tk
<<<<<<< HEAD
<<<<<<< HEAD
from tkinter import ttk, messagebox
import json
=======
from tkinter import ttk, messagebox, filedialog, simpledialog
import json, os
>>>>>>> main
=======
from tkinter import ttk, messagebox, filedialog, simpledialog
import json, os
>>>>>>> Uzair
from tkinter.scrolledtext import ScrolledText

class DialogueEntry:
    def __init__(self, root):
        self.root = root
        self.root.title("Dialogue Entry")
        
        self.dialogue = {
<<<<<<< HEAD
<<<<<<< HEAD
            "characterName": "",
            "sentences": []
        }
        
        self.current_sentence = None   
=======
=======
>>>>>>> Uzair
            "characters": []
        }
        
        self.characters = []
        self.current_sentence = None   
        self.selected_character = None
        self.selected_character_index = None
        self.selected_sentence = None
        self.selected_sentence_index = None
        self.selected_choice = None
        self.selected_choice_index = None
<<<<<<< HEAD
>>>>>>> main
=======
>>>>>>> Uzair

        self.create_widgets()
        self.update_json_display()

    def create_widgets(self):
<<<<<<< HEAD
<<<<<<< HEAD
=======
        """
>>>>>>> main
=======
        """
>>>>>>> Uzair
        # Frame for Character Name
        char_frame = ttk.Frame(self.root, padding="10")
        char_frame.grid(row=0, column=0, sticky="W")
        
        ttk.Label(char_frame, text="Character Name:").grid(row=0, column=0, sticky="W")
        self.char_entry = ttk.Entry(char_frame, width=30)
        self.char_entry.grid(row=0, column=1, padx=5)
        self.char_entry.bind("<FocusOut>", self.set_character_name)
<<<<<<< HEAD
<<<<<<< HEAD
        
        # Frame for Sentences
        sentence_frame = ttk.LabelFrame(self.root, text="Add Sentence", padding="10")
        sentence_frame.grid(row=1, column=0, padx=10, pady=5, sticky="EW")
=======
=======
>>>>>>> Uzair
        """
        
        # Frame for Characters
        char_manage_frame = ttk.LabelFrame(self.root, text="Manage Characters", padding="10")
        char_manage_frame.grid(row=1, column=0, padx=10, pady=5, sticky="EW")
        
        ttk.Label(char_manage_frame, text="Character Name:").grid(row=0, column=0, sticky="W")
        self.new_char_entry = ttk.Entry(char_manage_frame, width=30)
        self.new_char_entry.grid(row=0, column=1, padx=5, pady=2)
        
        add_char_btn = ttk.Button(char_manage_frame, text="Add Character", command=self.add_character)
        add_char_btn.grid(row=0, column=2, padx=5)
        
        # Listbox to display characters
        self.char_listbox = tk.Listbox(char_manage_frame, height=5)
        self.char_listbox.grid(row=1, column=0, columnspan=3, sticky="EW", pady=5)
        self.char_listbox.bind('<<ListboxSelect>>', self.on_character_select)
        
        # Frame for Sentences
        sentence_frame = ttk.LabelFrame(self.root, text="Manage Sentences", padding="10")
        sentence_frame.grid(row=2, column=0, padx=10, pady=5, sticky="EW")
<<<<<<< HEAD
>>>>>>> main
=======
>>>>>>> Uzair
        
        ttk.Label(sentence_frame, text="Sentence Text:").grid(row=0, column=0, sticky="W")
        self.sentence_entry = ttk.Entry(sentence_frame, width=50)
        self.sentence_entry.grid(row=0, column=1, padx=5, pady=2)
        
        add_sentence_btn = ttk.Button(sentence_frame, text="Add Sentence", command=self.add_sentence)
        add_sentence_btn.grid(row=0, column=2, padx=5)
        
<<<<<<< HEAD
<<<<<<< HEAD
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
=======
=======
>>>>>>> Uzair
        # Listbox to display sentences
        self.sentence_listbox = tk.Listbox(sentence_frame, height=5)
        self.sentence_listbox.grid(row=1, column=0, columnspan=3, sticky="EW", pady=5)
        self.sentence_listbox.bind('<<ListboxSelect>>', self.on_sentence_select)
        
        # Buttons for editing sentences
        edit_sentence_btn = ttk.Button(sentence_frame, text="Edit Sentence", command=self.edit_sentence)
        edit_sentence_btn.grid(row=2, column=0, pady=5)
        
        remove_sentence_btn = ttk.Button(sentence_frame, text="Remove Sentence", command=self.remove_sentence)
        remove_sentence_btn.grid(row=2, column=1, pady=5)
        
        # Frame for Choices Management
        choices_manage_frame = ttk.LabelFrame(self.root, text="Manage Choices", padding="10")
        choices_manage_frame.grid(row=3, column=0, padx=10, pady=5, sticky="EW")
        
        ttk.Label(choices_manage_frame, text="Choice Text:").grid(row=0, column=0, sticky="W")
        self.choice_text_entry = ttk.Entry(choices_manage_frame, width=50)
        self.choice_text_entry.grid(row=0, column=1, padx=5, pady=2)
        
        ttk.Label(choices_manage_frame, text="Next Dialogue Character:").grid(row=1, column=0, sticky="W")
        self.choice_next_char_entry = ttk.Entry(choices_manage_frame, width=30)
        self.choice_next_char_entry.grid(row=1, column=1, padx=5, pady=2)
        
        ttk.Label(choices_manage_frame, text="Next Sentence Text:").grid(row=2, column=0, sticky="W")
        self.choice_next_sentence_entry = ttk.Entry(choices_manage_frame, width=50)
        self.choice_next_sentence_entry.grid(row=2, column=1, padx=5, pady=2)
        
        add_choice_btn = ttk.Button(choices_manage_frame, text="Add Choice", command=self.add_choice)
        add_choice_btn.grid(row=3, column=1, pady=5, sticky="E")
        
        # Listbox to display choices
        self.choice_listbox = tk.Listbox(choices_manage_frame, height=5)
        self.choice_listbox.grid(row=4, column=0, columnspan=3, sticky="EW", pady=5)
        self.choice_listbox.bind('<<ListboxSelect>>', self.on_choice_select)
        
        # Buttons for editing choices
        edit_choice_btn = ttk.Button(choices_manage_frame, text="Edit Choice", command=self.edit_choice)
        edit_choice_btn.grid(row=5, column=0, pady=5)
        
        remove_choice_btn = ttk.Button(choices_manage_frame, text="Remove Choice", command=self.remove_choice)
        remove_choice_btn.grid(row=5, column=1, pady=5)

        # Frame for State Number
        state_frame = ttk.Frame(self.root, padding="10")
        state_frame.grid(row=4, column=0, sticky="W")

        ttk.Label(state_frame, text="State Number:").grid(row=0, column=0, sticky="W")
        self.state_entry = ttk.Entry(state_frame, width=10)
        self.state_entry.grid(row=0, column=1, padx=5)
        self.state_entry.insert(0, "0")  # Default state number
        self.state_entry.bind("<FocusOut>", lambda e: self.save_json_to_file())

        # Label to display current file name
        self.filename_label = ttk.Label(self.root, text="Current File: None")
        self.filename_label.grid(row=5, column=0, padx=10, pady=5, sticky="W")

        # Button to load existing dialogue
        load_button = ttk.Button(self.root, text="Load Dialogue", command=self.load_json_file)
        load_button.grid(row=6, column=0, padx=10, pady=5, sticky="W")
        
        # JSON Display
        json_frame = ttk.LabelFrame(self.root, text="JSON Output", padding="10")
        json_frame.grid(row=7, column=0, padx=10, pady=5, sticky="NSEW")
<<<<<<< HEAD
>>>>>>> main
=======
>>>>>>> Uzair
        
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

<<<<<<< HEAD
<<<<<<< HEAD
    def add_sentence(self):
=======
=======
>>>>>>> Uzair
    def add_character(self):
        char_name = self.new_char_entry.get().strip()
        if not char_name:
            messagebox.showwarning("Input Error", "Character name cannot be empty.")
            return
        # Check for duplicate character names
        for char in self.dialogue["characters"]:
            if char["characterName"] == char_name:
                messagebox.showwarning("Duplicate Character", "Character already exists.")
                return
        
        self.characters.append(char_name)
        new_char = {
            "characterName": char_name,
            "sentences": []
        }
        self.dialogue["characters"].append(new_char)
        self.char_listbox.insert(tk.END, char_name)
        self.new_char_entry.delete(0, tk.END)
        self.update_json_display()

    def on_character_select(self, event):
        selection = self.char_listbox.curselection()
        if selection:
            index = selection[0]
            self.selected_character = self.dialogue["characters"][index]
            self.selected_character_index = index#[i for i in range(len(self.characters)) if self.characters[i] == self.selected_character][0]
            self.populate_sentences()
        else:
            self.selected_character = None
            #self.selected_character_index = None
            #self.sentence_listbox.delete(0, tk.END)
            #self.choice_listbox.delete(0, tk.END)
            #self.selected_sentence = None
            #self.selected_sentence_index = None
            #self.choice_text_entry.delete(0, tk.END)
            #self.choice_next_char_entry.delete(0, tk.END)
            #self.choice_next_sentence_entry.delete(0, tk.END)

    def populate_sentences(self):
        self.sentence_listbox.delete(0, tk.END)
        if self.selected_character:
            for sentence in self.selected_character["sentences"]:
                self.sentence_listbox.insert(tk.END, sentence["text"])

    def add_sentence(self):
        if not self.selected_character:
            messagebox.showwarning("No Character Selected", "Please select a character to add sentences.")
            return
<<<<<<< HEAD
>>>>>>> main
=======
>>>>>>> Uzair
        text = self.sentence_entry.get().strip()
        if not text:
            messagebox.showwarning("Input Error", "Sentence text cannot be empty.")
            return
        sentence = {
            "text": text,
            "choices": []
        }
<<<<<<< HEAD
<<<<<<< HEAD
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
=======
=======
>>>>>>> Uzair
        self.selected_character["sentences"].append(sentence)
        self.sentence_listbox.insert(tk.END, text)
        self.sentence_entry.delete(0, tk.END)
        self.update_json_display()
        messagebox.showinfo("Success", "Sentence added.")

    def on_sentence_select(self, event):
        selection = self.sentence_listbox.curselection()
        if selection:
            index = selection[0]
            self.selected_sentence_index = index
            self.selected_sentence = self.selected_character["sentences"][index]
            self.populate_choices()
        else:
            #self.selected_sentence = None
            self.choice_listbox.delete(0, tk.END)

    def edit_sentence(self):
        if not self.selected_sentence:
            messagebox.showwarning("No Sentence Selected", "Please select a sentence to edit.")
            return
        new_text = simpledialog.askstring("Edit Sentence", "Enter new sentence text:", initialvalue=self.selected_sentence["text"])
        if new_text:
            self.selected_sentence["text"] = new_text.strip()
            selected_index = self.selected_sentence_index#self.sentence_listbox.curselection()[0]
            self.sentence_listbox.delete(selected_index)
            self.sentence_listbox.insert(selected_index, new_text.strip())
            self.sentence_listbox.select_set(selected_index)
            self.update_json_display()
            messagebox.showinfo("Success", "Sentence edited.")

    def remove_sentence(self):
        if not self.selected_sentence:
            messagebox.showwarning("No Sentence Selected", "Please select a sentence to remove.")
            return
        confirm = messagebox.askyesno("Confirm Removal", "Are you sure you want to remove the selected sentence?")
        if confirm:
            selected_index = self.sentence_listbox.curselection()[0]
            del self.dialogue["characters"][self.selected_character_index]["sentences"][selected_index]
            self.sentence_listbox.delete(selected_index)
            self.selected_sentence = None
            self.update_json_display()
            messagebox.showinfo("Success", "Sentence removed.")

    def highlight_json(self, sentence_index):
        # Optional: Implement highlighting in the JSON display
        # This can be complex with ScrolledText; consider using a different widget or color tags
        pass  # Placeholder for implementation

    def add_choice(self):
        if not self.selected_sentence:
            messagebox.showwarning("No Sentence Selected", "Please select a sentence to add choices.")
            return
        choice_text = self.choice_text_entry.get().strip()
        next_char = self.choice_next_char_entry.get().strip()
        next_sentence = self.choice_next_sentence_entry.get().strip()
<<<<<<< HEAD
>>>>>>> main
=======
>>>>>>> Uzair
        
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
        
<<<<<<< HEAD
<<<<<<< HEAD
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
=======
=======
>>>>>>> Uzair
        if self.selected_choice:
            # Edit existing choice
            self.selected_choice["choiceText"] = choice_text
            self.selected_choice["nextDialogue"]["characterName"] = next_char
            self.selected_choice["nextDialogue"]["sentences"][0]["text"] = next_sentence
            selected_index = self.selected_choice_index
            self.choice_listbox.delete(selected_index)
            self.choice_listbox.insert(selected_index, choice_text)
            self.choice_listbox.select_set(selected_index)
            self.selected_choice = self.selected_sentence["choices"][selected_index]
            self.selected_choice_index = selected_index
            messagebox.showinfo("Success", "Choice edited.")
        else:
            # Add new choice
            self.selected_sentence["choices"].append(choice)
            self.choice_listbox.insert(tk.END, choice_text)
            messagebox.showinfo("Success", "Choice added.")
        
        # Clear choice entry fields
        self.choice_text_entry.delete(0, tk.END)
        self.choice_next_char_entry.delete(0, tk.END)
        self.choice_next_sentence_entry.delete(0, tk.END)
        self.update_json_display()

    def edit_choice(self):
        if not self.selected_choice:
            messagebox.showwarning("No Choice Selected", "Please select a choice to edit.")
            return
        # The add_choice function handles both adding and editing
        self.add_choice()

    def remove_choice(self):
        if not self.selected_choice:
            messagebox.showwarning("No Choice Selected", "Please select a choice to remove.")
            return
        confirm = messagebox.askyesno("Confirm Removal", "Are you sure you want to remove the selected choice?")
        if confirm:
            selected_index = self.choice_listbox.curselection()[0]
            del self.selected_sentence["choices"][selected_index]
            self.choice_listbox.delete(selected_index)
            self.selected_choice = None
            self.selected_choice_index = None
            # Clear choice entry fields
            self.choice_text_entry.delete(0, tk.END)
            self.choice_next_char_entry.delete(0, tk.END)
            self.choice_next_sentence_entry.delete(0, tk.END)
            self.update_json_display()
            messagebox.showinfo("Success", "Choice removed.")

    def populate_choices(self):
        self.choice_listbox.delete(0, tk.END)
        if self.selected_sentence:
            for choice in self.selected_sentence.get("choices", []):
                self.choice_listbox.insert(tk.END, choice["choiceText"])

    def on_choice_select(self, event):
        selection = self.choice_listbox.curselection()
        if selection:
            index = selection[0]
            self.selected_choice_index = index
            self.selected_choice = self.selected_sentence["choices"][index]
            # Autofill the choice entry fields
            self.choice_text_entry.delete(0, tk.END)
            self.choice_text_entry.insert(0, self.selected_choice.get("choiceText", ""))
            self.choice_next_char_entry.delete(0, tk.END)
            self.choice_next_char_entry.insert(0, self.selected_choice.get("nextDialogue", {}).get("characterName", ""))
            self.choice_next_sentence_entry.delete(0, tk.END)
            self.choice_next_sentence_entry.insert(0, self.selected_choice.get("nextDialogue", {}).get("sentences", [{}])[0].get("text", ""))
        else:
            self.selected_choice = None
            self.selected_choice_index = None
            self.choice_text_entry.delete(0, tk.END)
            self.choice_next_char_entry.delete(0, tk.END)
            self.choice_next_sentence_entry.delete(0, tk.END)

    def generate_filename(self):
        characters = [char["characterName"] for char in self.dialogue["characters"]]
        state_number = self.state_entry.get().strip()
        if not state_number.isdigit():
            messagebox.showwarning("Input Error", "State number must be a number.")
            return None
        filename = "_".join(characters) + f"_S{state_number}.json"
        return filename
    
    def save_json_to_file(self):
        filename = self.generate_filename()
        if not filename:
            return
        # Define the directory path
        base_dir = os.path.dirname(os.path.dirname(os.path.dirname(os.path.abspath(__file__))))
        #print(base_dir)
        dialogue_dir = os.path.join(base_dir, "Resources", "Dialogue")
        #os.makedirs(dialogue_dir, exist_ok=True)
        file_path = os.path.join(dialogue_dir, filename)
        with open(file_path, 'w') as f:
            json.dump(self.dialogue, f, indent=4)
        self.filename_label.config(text=f"Current File: {filename}")

    def load_json_file(self):
        base_dir = os.path.dirname(os.path.dirname(os.path.dirname(os.path.abspath(__file__))))
        dialogue_dir = os.path.join(base_dir, "Resources", "Dialogue")
        os.makedirs(dialogue_dir, exist_ok=True)
        file_path = filedialog.askopenfilename(
            initialdir=dialogue_dir,
            title="Select Dialogue JSON File",
            filetypes=(("JSON files", "*.json"), ("All files", "*.*"))
        )
        if file_path:
            with open(file_path, 'r') as f:
                self.dialogue = json.load(f)

            # Update character listbox
            self.char_listbox.delete(0, tk.END)
            self.characters.clear()
            for char in self.dialogue.get("characters", []):
                char_name = char.get("characterName", "")
                self.char_listbox.insert(tk.END, char_name)
                self.characters.append(char_name)
            print(self.characters)

            # Update state number based on filename
            basename = os.path.basename(file_path)
            parts = basename.split("_S")
            if len(parts) == 2 and parts[1].endswith(".json"):
                state_num = parts[1][:-5]
                self.state_entry.delete(0, tk.END)
                self.state_entry.insert(0, state_num)
            self.populate_sentences()
            self.update_json_display()
            self.filename_label.config(text=f"Loaded File: {basename}")

            # Clear choices
            self.choice_listbox.delete(0, tk.END)
            self.selected_choice = None
            self.selected_choice_index = None
            self.choice_text_entry.delete(0, tk.END)
            self.choice_next_char_entry.delete(0, tk.END)
            self.choice_next_sentence_entry.delete(0, tk.END)

    def update_json_display(self):
        self.json_display.configure(state='normal')
        pretty_json = json.dumps(self.dialogue, indent=4)
        self.json_display.delete(1.0, tk.END)
        self.json_display.insert(tk.END, pretty_json)
        self.json_display.configure(state='disabled')
        self.save_json_to_file()
<<<<<<< HEAD
>>>>>>> main
=======
>>>>>>> Uzair

if __name__ == "__main__":
    root = tk.Tk()
    app = DialogueEntry(root)
    root.mainloop()