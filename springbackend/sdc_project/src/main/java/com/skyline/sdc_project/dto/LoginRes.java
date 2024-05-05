package com.skyline.sdc_project.dto;

import java.util.ArrayList;
import java.util.List;

public class LoginRes {
    private String username;
    private String token;
    private List<String> roles;

    private Integer id;

    public LoginRes(String username, String token,List<String> roles,Integer id) {
        this.username = username;
        this.token = token;
        this.roles = roles;
        this.id=id;
    }

    public String getUsername() {
        return username;
    }

    public void setUsername(String username) {
        this.username = username;
    }

    public String getToken() {
        return token;
    }

    public void setToken(String token) {
        this.token = token;
    }

    public Integer getId() {
        return id;
    }

    public void setId(Integer id) {
        this.id = id;
    }

    public List<String> getRoles() {
        return roles;
    }

    public void setRoles(List<String> roles) {
        this.roles = roles;
    }

}
