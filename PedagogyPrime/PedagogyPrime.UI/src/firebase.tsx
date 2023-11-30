// Import the functions you need from the SDKs you need
import { getFirestore } from "firebase/firestore";
import { initializeApp } from "firebase/app";
import { getStorage } from "firebase/storage";
import { key, messagingSenderId, appId, measurementId } from "./confidential";
const firebaseConfig = {
  apiKey: key,
  authDomain: "pedagogy-prime.firebaseapp.com",
  projectId: "pedagogy-prime",
  storageBucket: "pedagogy-prime.appspot.com",
  messagingSenderId: messagingSenderId,
  appId: appId,
  measurementId: measurementId,
};
// Initialize Firebase
const app = initializeApp(firebaseConfig);
export const storage = getStorage(app);
export const firestore = getFirestore(app);
