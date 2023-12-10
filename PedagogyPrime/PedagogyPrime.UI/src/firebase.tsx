// Import the functions you need from the SDKs you need
import { getFirestore } from "firebase/firestore";
import { initializeApp } from "firebase/app";
import { getStorage } from "firebase/storage";
const firebaseConfig = {
  apiKey: "AIzaSyDPoku4vPkx0cKTh-55bz-NNn1MnbyA88A",
  authDomain: "pedagogy-prime.firebaseapp.com",
  projectId: "pedagogy-prime",
  storageBucket: "pedagogy-prime.appspot.com",
  messagingSenderId: "283690466019",
  appId: "1:283690466019:web:2832ab626e04fa4527e3d6",
  measurementId: "G-8SNK8HYQ7E",
};
// Initialize Firebase
const app = initializeApp(firebaseConfig);
export const storage = getStorage(app);
export const firestore = getFirestore(app);
