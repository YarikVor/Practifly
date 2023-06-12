import React, { useState } from "react";
import { Checkbox, Box, Typography, List, ListItem, Button } from "@mui/material";

import { useStyles } from "./styles";

interface Rubric {
    id: number;
    name: string;
}

interface RubricsMenuProps {
    rubrics: Rubric[];
}

const RubricsMenu: React.FC<RubricsMenuProps> = ({ rubrics }) => {
  const classes = useStyles();
  const [selectedRubrics, setSelectedRubrics] = useState<Rubric[]>([]);

  const handleRubricClick = (rubric: Rubric) => {
    const isSelected = selectedRubrics.some((selected) => selected.id === rubric.id);

    if (isSelected) {
      setSelectedRubrics(selectedRubrics.filter((selected) => selected.id !== rubric.id));
    } else {
      setSelectedRubrics([...selectedRubrics, rubric]);
    }
  };

  const isRubricSelected = (rubric: Rubric) => selectedRubrics.some((selected) => selected.id === rubric.id);

  return (
    <Box className={classes.container}>
      <div className={classes.menuHeader}>
        <Typography variant="subtitle1" className={classes.menuHeaderText}>
                    Меню рубрик
        </Typography>
      </div>
      <List>
        {rubrics.map((rubric, index) => (
          <React.Fragment key={rubric.id}>
            <ListItem
              className={`${classes.rubricItem} ${
                isRubricSelected(rubric) ? classes.selectedRubric : ""
              }`}
              onClick={() => handleRubricClick(rubric)}
              
            >
              <Checkbox
                checked={isRubricSelected(rubric)}
                onChange={() => handleRubricClick(rubric)}
                color="success"
                size="small"

              />
              {rubric.name}
            </ListItem>
            {index !== rubrics.length - 1 && <hr className={classes.divider} />}
          </React.Fragment>
        ))}
      </List>
      <div className={classes.buttons}>
        <Button
          className={classes.button}
          variant="contained"
          style={{ backgroundColor: "#9062F2", color: "#FFFFFF" }}
        >
                    Детально
        </Button>
        <Button
          className={classes.button}
          variant="contained"
          style={{ backgroundColor: "#9062F2", color: "#FFFFFF" }}
        >
                    Керування
        </Button>
        <Button
          className={classes.button}
          variant="contained"
          style={{ backgroundColor: "#9062F2", color: "#FFFFFF" }}
        >
                    Використання
        </Button>
        <Button
          className={classes.button}
          variant="contained"
          style={{ backgroundColor: "#9062F2", color: "#FFFFFF" }}
        >
                    Матеріали
        </Button>
        <Button
          className={classes.button}
          variant="contained"
          style={{ backgroundColor: "#9062F2", color: "#FFFFFF" }}
        >
                    Курси
        </Button>
      </div>
    </Box>
  );
};

export default RubricsMenu;
