package main

import (
	"encoding/json"
	"fmt"
	"net/http"
)

type Response struct {
	Status string `json:"message"`
}

func handler(w http.ResponseWriter, r *http.Request) {
	w.Header().Set("Content-Type", "application/json")
	response := Response{Status: "OK"}
	if err := json.NewEncoder(w).Encode(response); err != nil {
		http.Error(w, err.Error(), http.StatusInternalServerError)
	}
}

func main() {
	http.HandleFunc("/health", handler)
	fmt.Println("Starting server on: 80")
	if err := http.ListenAndServe(":80", nil); err != nil {
		fmt.Println("Error starting server: ", err)
	}
}
