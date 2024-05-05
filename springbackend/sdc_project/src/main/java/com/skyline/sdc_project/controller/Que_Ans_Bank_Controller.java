package com.skyline.sdc_project.controller;
import com.skyline.sdc_project.entity.que_ans_bank;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.jdbc.core.JdbcTemplate;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;
import org.springframework.web.bind.annotation.*;
import java.util.List;

@RestController
@CrossOrigin(origins = "*")
@RequestMapping("/api/questions")
public class Que_Ans_Bank_Controller {

    @Autowired
    private JdbcTemplate jdbcTemplate;

    // Get all questions
    @GetMapping
    public List<que_ans_bank> getAllQuestions() {
        return jdbcTemplate.query("SELECT * FROM que_ans_bank", (resultSet, rowNum) ->
                new que_ans_bank(resultSet.getLong("id"), resultSet.getString("que")));
    }
    // Get a specific question by ID
    @GetMapping("/{id}")
    public ResponseEntity<que_ans_bank> getQuestionById(@PathVariable Long id) {
        String sql = "SELECT * FROM que_ans_bank WHERE id = ?";
        que_ans_bank question = jdbcTemplate.queryForObject(sql, new Object[]{id}, (resultSet, rowNum) ->
                new que_ans_bank(resultSet.getLong("id"), resultSet.getString("que")));
        return question != null ? ResponseEntity.ok(question) : ResponseEntity.notFound().build();
    }

}
