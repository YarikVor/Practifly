import React, { useState } from "react";
import { TextField, Button } from "@mui/material";

interface SearchCoursesProps {
    totalCourses: 100;
}

const SearchCourses: React.FC<SearchCoursesProps> = ({ totalCourses }) => {
  const [searchQuery, setSearchQuery] = useState("");

  const handleSearch = () => {
    // Логіка пошуку курсів з використанням searchQuery
    console.log(`Запит на пошук: ${searchQuery}`);
  };

  return (
    <div style={{ width: "100%", marginTop: "20px", marginBottom: "20px" }}>
      <TextField
        label="Пошук курсів"
        value={searchQuery}
        onChange={(e) => setSearchQuery(e.target.value)}
        fullWidth // Розтягує поле по ширині
      />
      <Button variant="contained" onClick={handleSearch} style={{ marginTop: "10px" }}>
                Шукати
      </Button>
      <p>Загальна кількість курсів: {totalCourses}</p>
    </div>
  );
};

export default SearchCourses;
