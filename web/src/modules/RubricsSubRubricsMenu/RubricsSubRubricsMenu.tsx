import React, { useState } from "react";
import { Box, Typography, List, ListItem, Button, Collapse } from "@mui/material";

import { useStyles } from "./styles";

interface Rubric {
    id: number;
    name: string;
    subRubrics?: Rubric[];
}

interface RubricsMenuProps {
    rubrics: Rubric[];
}

const RubricsMenu: React.FC<RubricsMenuProps> = ({ rubrics }) => {
  const classes = useStyles();
  const [selectedRubrics, setSelectedRubrics] = useState<Rubric[]>([]);

  const handleRubricClick = (rubric: Rubric) => {
    const isSelected = selectedRubrics.some((selected) => selected.name === rubric.name);

    if (isSelected) {
      setSelectedRubrics(selectedRubrics.filter((selected) => selected.name !== rubric.name));
    } else {
      setSelectedRubrics([...selectedRubrics, rubric]);
    }
  };

  const isRubricSelected = (rubric: Rubric) =>
    selectedRubrics.some((selected) => selected.name === rubric.name);

  const renderRubric = (rubric: Rubric) => (
    <React.Fragment key={rubric.id}>
      <ListItem
        className={`${classes.rubricItem} ${isRubricSelected(rubric) ? classes.selectedRubric : ""}`}
        onClick={() => handleRubricClick(rubric)}
      >
        {rubric.subRubrics ? (
          <Button
            style={{ color: "#000000" }}
            onClick={() => handleRubricClick(rubric)}
          >
            {rubric.name}
          </Button>
        ) : (
          <Typography>{rubric.name}</Typography>
        )}
      </ListItem>
      {rubric.subRubrics && (
        <Collapse in={isRubricSelected(rubric)}>
          <List className={classes.subRubricsList}>
            {rubric.subRubrics.map((subRubric) => (
              <ListItem
                key={subRubric.id}
                className={`${classes.rubricItem} ${
                  isRubricSelected(subRubric) ? classes.selectedRubric : ""
                }`}
                onClick={() => handleRubricClick(subRubric)}
              >
                <Typography>{subRubric.name}</Typography>
              </ListItem>
            ))}
          </List>
        </Collapse>
      )}
    </React.Fragment>
  );

  return (
    <Box className={classes.container}>
      <div className={classes.menuHeader}>
        <Typography variant="subtitle1" className={classes.menuHeaderText}>
                     Рубрики
        </Typography>
      </div>
      <List>{rubrics.map(renderRubric)}</List>
    </Box>
  );
};

export default RubricsMenu;
